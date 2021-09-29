import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { User } from 'src/app/shared/models/user';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { UserService } from 'src/app/core/services/user.service';
import { UpdateUserComponent } from '../../dialogs/update-user/update-user.component';
import { AddUserComponent } from '../../dialogs/add-user/add-user.component';
import { DeleteUserComponent } from '../../dialogs/delete-user/delete-user.component';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit, AfterViewInit{

  accessToken = '';
  refreshToken = '';
  public users: User[];
  public id: number;
  public newUser: User;
  public dataSource = new MatTableDataSource<User>();

  displayedColumns: string[] = ['name', 'surname', 'email', 'adress', 'dateOfBirth', 'actions'];
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  constructor(public authService: AuthService, public dialog: MatDialog, private userService: UserService) { }

  async ngOnInit(): Promise<void> {
    this.accessToken = localStorage.getItem('access_token');
    this.refreshToken = localStorage.getItem('refresh_token');
    this.authService.checkIfLogedIn();
    await this.getUsers();
  }

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  async getUsers(): Promise<void> {
    this.users = await this.userService.getUsers().toPromise();
    this.dataSource.data = this.users;
  }

  editUsers(user: User, name: string, surname: string, email: string, dateOfBirth: Date, adress: string)
  : void {
    this.id = user.id;
    console.log(this.id);
    const dialogRef = this.dialog.open(UpdateUserComponent, {
      data: {id: this.id, name, surname, email, dateOfBirth, adress}
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        this.refreshList(user);
        this.users.push(this.userService.newUser);
        this.updateDataSource();
      }
    });
  }

  addNewUser(): void {
    const dialogRef = this.dialog.open(UpdateUserComponent, {
      data: { name: ' ', surname: ' ', email: ' ', dateOfBirth: ' ', adress: ' '}
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        this.users.push(this.userService.newUser);
        this.updateDataSource();
      }
    });
  }

  deleteUser(user: User, name: string, surname: string): void {
    this.id = user.id;
    console.log(this.id);
    const dialogRef = this.dialog.open(DeleteUserComponent, {
      data: {id: this.id, name, surname}
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result === 1) {
        this.refreshList(user);
        this.updateDataSource();
      }
    });
  }

  private refreshList(user: User): void {
    this.users = this.users.filter(s => s !== user);
  }

  private updateDataSource(): void {
    this.dataSource.data = this.users;
    this.dataSource.filter = '';
  }
}
