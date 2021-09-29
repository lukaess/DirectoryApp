import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-za-probu',
  templateUrl: './za-probu.component.html',
  styleUrls: ['./za-probu.component.scss']
})
export class ZaProbuComponent implements OnInit {

  constructor(public authService: AuthService) { }

  ngOnInit(): void {
    this.authService.checkIfLogedIn();
  }

}
