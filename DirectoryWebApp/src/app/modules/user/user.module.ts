import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './components/user/user.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { ZaProbuComponent } from './components/za-probu/za-probu.component';
import { UpdateUserComponent } from './dialogs/update-user/update-user.component';
import { AddUserComponent } from './dialogs/add-user/add-user.component';
import { DeleteUserComponent } from './dialogs/delete-user/delete-user.component';

const routes: Routes = [
  { path: '', redirectTo: '/userApp', pathMatch: 'full' },
  { path: 'userApp', component: UserComponent},
  { path: 'proba', component: ZaProbuComponent},
];

@NgModule({
  declarations: [UserComponent, ZaProbuComponent, UpdateUserComponent, AddUserComponent, DeleteUserComponent],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule]
})
export class UserModule { }
