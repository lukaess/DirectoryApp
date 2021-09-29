import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AuthService } from 'src/app/core/services/auth.service';
import { UserService } from 'src/app/core/services/user.service';
import { User } from 'src/app/shared/models/user';
import { DeleteUserComponent } from '../delete-user/delete-user.component';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.scss']
})
export class UpdateUserComponent {

  constructor(public dialogRef: MatDialogRef<UpdateUserComponent>, @Inject(MAT_DIALOG_DATA) public data: any,
              private userService: UserService, public authService: AuthService) { }

  formControl = new FormControl('', [
    Validators.required
  ]);

  getErrorMessage(): '' | 'Required field' {
    return this.formControl.hasError('required') ? 'Required field' :
        '';
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  submit(): void {
  }

  async stopEdit(): Promise<void> {
    const user = this.data as User;
    if (user.id)
    {
      await this.userService.updateUser(user).toPromise();
    }
    else
    {
      await this.userService.addUser(user).toPromise();
    }
  }

}
