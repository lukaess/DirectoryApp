import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { finalize } from 'rxjs/operators';
import { Subscription } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  form: FormGroup;
  busy = false;
  username = '';
  password = '';
  loginError = false;
  private subscription: Subscription;
  public loginInvalid: boolean;
  private formSubmitAttempt: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private fb: FormBuilder,
    ) { }

  ngOnInit(): void {
    this.subscription = this.authService.user$.subscribe((x) => {
      if (this.route.snapshot.url[0].path === 'login') {
        const accessToken = localStorage.getItem('access_token');
        const refreshToken = localStorage.getItem('refresh_token');
        if (x && accessToken && refreshToken) {
          const returnUrl = this.route.snapshot.queryParams.returnUrl || '/users';
          this.router.navigate([returnUrl]);
        }
      }
    });
    this.createForm();
  }
  createForm(): void {
    this.form = this.fb.group({
      email: ['', Validators.email],
      password: ['', Validators.required],
    });
  }

  async onSubmit(): Promise<void> {
    const returnUrl = this.route.snapshot.queryParams.returnUrl || '/users/userApp';
    this.loginInvalid = false;
    this.formSubmitAttempt = false;
    if (this.form.valid) {
      try {
        const email = this.form.get('email').value;
        const password = this.form.get('password').value;
        await this.authService.login(email, password).subscribe(
          () => {
            this.router.navigate([returnUrl]);
          },
          () => {
            this.loginInvalid = true;
          }
        );
      } catch (err) {
        this.loginInvalid = true;
      }
    } else {
      this.formSubmitAttempt = true;
    }
  }

  OnDestroy(): void {
    this.subscription?.unsubscribe();
  }
}

