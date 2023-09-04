import { Injectable } from '@angular/core';
import { GenericHttpService } from '../../shared/services/http-service';
import { BehaviorSubject, Observable, map, of, shareReplay, tap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from 'src/app/types/User.type';
import { Store } from '@ngrx/store';
import * as AuthActions from '../../state/auth/auth.actions';

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
  private subject = new BehaviorSubject<User | null>(null);
  isLoggedIn$: Observable<boolean>;
  user$: Observable<User | null> = this.subject.asObservable();
  constructor(
    private httpService: GenericHttpService,
    private jwtHelperService: JwtHelperService,
    private store: Store
  ) {
    this.isTokenExpired();
    this.isLoggedIn$ = this.user$.pipe(map((user) => !!user));
  }

  isTokenExpired(): boolean {
    if (this.jwtHelperService.isTokenExpired(localStorage.getItem('token'))) {
      this.isLoggedIn$ = of(false);
      localStorage.removeItem('token');
      return true;
    }

    return false;
  }

  saveUser(token: string) {
    const decodedToken = this.jwtHelperService.decodeToken(token);

    if (
      !decodedToken[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'
      ]
    )
      return;

    return this.httpService
      .get<User>(
        `User/${decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']}`
      )
      .pipe(
        tap((user) => this.subject.next(user)),
        shareReplay()
      );
  }

  login(email: string, password: string): Observable<any> {
    return this.httpService.post<Login>('User/login', { email, password });
  }

  register(user: Register): Observable<any> {
    return this.httpService.post<Register>('User', user);
  }

  logout() {
    this.subject.next(null);
  }
}
