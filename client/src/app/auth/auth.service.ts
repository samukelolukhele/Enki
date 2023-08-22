import { Injectable } from '@angular/core';
import { GenericHttpService } from '../shared/services/http-service';
import { BehaviorSubject, tap } from 'rxjs';

interface Login {
  email: string;
  password: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isLoggedIn = new BehaviorSubject<boolean>(false);
  constructor(private httpService: GenericHttpService) {
    const token = localStorage.getItem('token');
    this.isLoggedIn.next(!!token);
  }

  login(email: string, password: string) {
    this.httpService
      .post<Login>('User/login', { email, password })
      .pipe(
        tap((res: any) => {
          localStorage.setItem('token', `bearer ${JSON.stringify(res.value)}`);
          this.isLoggedIn.next(true);
        })
      )
      .subscribe();
  }
}
