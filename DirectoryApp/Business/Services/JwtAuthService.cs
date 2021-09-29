namespace Business.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    public class JwtAuthService : IJwtAuthService
    {
        public IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary => this.usersRefreshTokens.ToImmutableDictionary();

        private readonly ConcurrentDictionary<string, RefreshToken> usersRefreshTokens;  // can store in a database or a distributed cache
        private readonly byte[] secret;
        private readonly IConfiguration config;

        public JwtAuthService(IConfiguration config)
        {
            this.config = config;
            this.usersRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();
            this.secret = Encoding.ASCII.GetBytes(this.config["AppSettings:Token"]);
        }

        // optional: clean up expired refresh tokens
        public void RemoveExpiredRefreshTokens(DateTime now)
        {
            var expiredTokens = this.usersRefreshTokens.Where(x => x.Value.ExpireAt < now).ToList();
            foreach (var expiredToken in expiredTokens)
            {
                this.usersRefreshTokens.TryRemove(expiredToken.Key, out _);
            }
        }

        // can be more specific to ip, user agent, device name, etc.
        public void RemoveRefreshTokenByUserName(string userName)
        {
            var refreshTokens = this.usersRefreshTokens.Where(x => x.Value.UserName == userName).ToList();
            foreach (var refreshToken in refreshTokens)
            {
                this.usersRefreshTokens.TryRemove(refreshToken.Key, out _);
            }
        }

        public JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                this.config["AppSettings:issuer"],
                shouldAddAudienceClaim ? this.config["AppSettings:audience"] : string.Empty,
                claims,
                expires: now.AddMinutes(2),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(this.secret), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshToken
            {
                UserName = username,
                TokenString = GenerateRefreshTokenString(),
                ExpireAt = now.AddMinutes(2),
            };
            this.usersRefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);

            return new JwtAuthResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        // public JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now)
        // {
        //    var (principal, jwtToken) = DecodeJwtToken(accessToken);
        //    if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
        //    {
        //        throw new SecurityTokenException("Invalid token");
        //    }

        // var userName = principal.Identity.Name;
        //    if (!usersRefreshTokens.TryGetValue(refreshToken, out var existingRefreshToken))
        //    {
        //        throw new SecurityTokenException("Invalid token");
        //    }
        //    if (existingRefreshToken.UserName != userName || existingRefreshToken.ExpireAt < now)
        //    {
        //        throw new SecurityTokenException("Invalid token");
        //    }

        // return GenerateTokens(userName, principal.Claims.ToArray(), now); // need to recover the original claims
        // }

        // public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        // {
        //    if (string.IsNullOrWhiteSpace(token))
        //    {
        //        throw new SecurityTokenException("Invalid token");
        //    }
        //    var principal = new JwtSecurityTokenHandler()
        //        .ValidateToken(token,
        //            new TokenValidationParameters
        //            {
        //                ValidateIssuer = true,
        //                ValidIssuer = _jwtTokenConfig.Issuer,
        //                ValidateIssuerSigningKey = true,
        //                IssuerSigningKey = new SymmetricSecurityKey(secret),
        //                ValidAudience = _jwtTokenConfig.Audience,
        //                ValidateAudience = true,
        //                ValidateLifetime = true,
        //                ClockSkew = TimeSpan.FromMinutes(1)
        //            },
        //            out var validatedToken);
        //    return (principal, validatedToken as JwtSecurityToken);
        // }
        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now)
        {
            throw new NotImplementedException();
        }

        public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
