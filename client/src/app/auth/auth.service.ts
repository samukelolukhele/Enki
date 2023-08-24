import { Injectable } from '@angular/core';
import { GenericHttpService } from '../shared/services/http-service';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from '../types/User.type';

interface Login {
  email: string;
  password: string;
}

interface Register extends Login {
  fName: string;
  lName: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isLoggedIn = new BehaviorSubject<boolean>(false);
  public token: string = localStorage.getItem('token') || '';
  public userId = new BehaviorSubject<string>('');
  constructor(
    private httpService: GenericHttpService,
    private jwtHelperService: JwtHelperService
  ) {
    this.isLoggedIn.next(!!this.token);
  }

  checkTokenExpiration() {
    if (this.jwtHelperService.isTokenExpired(this.token)) {
      this.isLoggedIn.next(false);
      localStorage.removeItem('token');
      this.token = '';
    } else {
      return;
    }
  }

  saveUserData() {
    const decodedToken = this.jwtHelperService.decodeToken(this.token);

    if (
      !decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
      ]
    )
      return this.userId.next('');

    return this.userId.next(
      decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
      ]
    );
  }

  login(email: string, password: string): Observable<any> {
    return this.httpService.post<Login>('User/login', { email, password }).pipe(
      tap((res: any) => {
        localStorage.setItem('token', `bearer ${res.value}`);
        this.isLoggedIn.next(true);
        this.saveUserData();
      })
    );
  }

  register(user: Register): Observable<Register> {
    return this.httpService
      .post<Register>('User', {
        fName: user.fName,
        lName: user.lName,
        email: user.email,
        password: user.password,
      })
      .pipe(
        tap((res: any) => {
          localStorage.setItem('token', `bearer ${res.value}`);
          this.isLoggedIn.next(true);
          this.saveUserData();
        })
      );
  }
}
