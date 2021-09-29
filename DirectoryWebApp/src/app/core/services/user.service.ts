import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { User } from 'src/app/shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  readonly apiUrl = 'https://localhost:44342/api/User';
  id: number;
  newUser: User;
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  dataChange: BehaviorSubject<User[]> = new BehaviorSubject<User[]>([]);
  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl)
      .pipe(
        catchError(this.errorHandler)
      );
  }

  addUser(user: User): Observable<User>{
    return this.http.post<User>(this.apiUrl, user, this.httpOptions).pipe(
      tap((newUser: User) => this.newUser = newUser),
      catchError(this.errorHandler)
    );
  }

  updateUser(user: User): Observable<any> {
    const id = typeof user === `number` ? user : user.id;
    const url = `${this.apiUrl}/${id}`;
    this.newUser = user;
    return this.http.put(url, user, this.httpOptions).pipe(
      catchError(this.errorHandler),
    );
  }

  deleteUser(user: User | number): Observable<User>{
    const id = typeof user === `number` ? user : user.id;
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<User>(url, this.httpOptions).pipe(
      catchError(this.errorHandler)
    );
  }

  errorHandler(error: HttpErrorResponse): Observable<never> {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  }
}
