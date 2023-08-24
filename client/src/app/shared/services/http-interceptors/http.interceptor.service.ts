import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SpinnerService } from '../spinner/spinner.service';
import { Observable, finalize } from 'rxjs';

@Injectable()
export class InterceptorService implements HttpInterceptor {
  constructor(public spinnerService: SpinnerService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.spinnerService.isLoading.next(true);

    return next.handle(req).pipe(
      finalize(() => {
        this.spinnerService.isLoading.next(false);
      })
    );
  }
}
