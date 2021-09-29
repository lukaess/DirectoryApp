import { Component } from '@angular/core';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'DirectoryWebApp';

  constructor(public authService: AuthService) {}

  ngOnInit(): void {
     this.authService.checkIfLogedIn();
  }

  logout(): void {
    this.authService.logout();
  }
}
