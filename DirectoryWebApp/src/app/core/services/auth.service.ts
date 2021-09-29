import { Injectable, OnDestroy } from '@angular/core';
import { LoginResult } from '../../shared/models/login-result';
import { AuthUser } from '../../shared/models/authUser';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of, Subscription } from 'rxjs';
import { map, tap, delay, finalize } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {

  readonly apiUrl = 'https://localhost:44342/api/User';
  private timer: Subscription;
  private userAdmin = new BehaviorSubject<AuthUser>(null);
  user$: Observable<AuthUser> = this.userAdmin.asObservable();

  private storageEventListener(event: StorageEvent): void {
    if (event.storageArea === localStorage) {
      if (event.key === 'logout-event') {
        this.stopTokenTimer();
        this.userAdmin.next(null);
      }
      if (event.key === 'login-event') {
        this.stopTokenTimer();
      }
    }
  }

  constructor(private router: Router, private http: HttpClient) {
    window.addEventListener('storage', this.storageEventListener.bind(this));
  }

  ngOnDestroy(): void {
    window.removeEventListener('storage', this.storageEventListener.bind(this));
  }

   login(email: string, password: string): Observable<LoginResult> {
    return this.http
      .post<LoginResult>(`${this.apiUrl}/login`, { email, password })
      .pipe(
        map((x) => {
          this.userAdmin.next({
            email: x.email,
            username: x.username,
            role: x.role,
          });
          this.setLocalStorage(x);
          this.startTokenTimer();
          return x;
        })
      );
  }

  logout(): void {
    this.http
      .post<unknown>(`${this.apiUrl}/logout`, {})
      .pipe(
        finalize(() => {
          this.clearLocalStorage();
          this.userAdmin.next(null);
          this.stopTokenTimer();
          this.router.navigate(['login']);
        })
      )
      .subscribe();
  }

  refreshToken(): Observable<any> {
    const refreshToken = localStorage.getItem('refresh_token');
    if (!refreshToken) {
      this.clearLocalStorage();
      return of(null);
    }
  }

  setLocalStorage(x: LoginResult): void {
    localStorage.setItem('access_token', x.accessToken);
    localStorage.setItem('refresh_token', x.refreshToken);
    localStorage.setItem('login-event', 'login' + Math.random());
    localStorage.setItem('email', x.email);
    localStorage.setItem('username', x.username);
    localStorage.setItem('role', x.role);
    localStorage.setItem('login-event', 'login' + Math.random());
  }

  clearLocalStorage(): void {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('email');
    localStorage.removeItem('username');
    localStorage.removeItem('role');
    localStorage.setItem('logout-event', 'logout' + Math.random());
  }

  private getTokenRemainingTime(): number {
    const accessToken = localStorage.getItem('access_token');
    if (!accessToken) {
      return 0;
    }
    const jwtToken = JSON.parse(atob(accessToken.split('.')[1]));
    const expires = new Date(jwtToken.exp * 1000);
    return expires.getTime() - Date.now();
  }

  private startTokenTimer(): void {
    const timeout = this.getTokenRemainingTime();
    this.timer = of(true)
      .pipe(
        delay(timeout),
        tap(() => this.refreshToken().subscribe())
      )
      .subscribe();
  }

  private stopTokenTimer(): void {
    this.timer?.unsubscribe();
  }

  checkIfLogedIn(): boolean {
    if (localStorage.email){
      this.userAdmin.next({
        email: localStorage.getItem('email'),
        role: localStorage.getItem('role'),
        username: localStorage.getItem('username'),
      });
      return true;
    }
    return false;
  }
}
