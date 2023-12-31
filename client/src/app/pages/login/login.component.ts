import {
  Component,
  EventEmitter,
  OnInit,
  AfterViewInit,
  ViewChildren,
  ElementRef,
} from '@angular/core';
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from '@angular/forms';
import { GenericValidator } from '../../shared/generic-validator';
import { Observable, debounceTime, fromEvent, merge, tap } from 'rxjs';
import { GenericHttpService } from '../../shared/services/http-service';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { User } from '../../types/User.type';
import { AuthService } from '../../auth/auth.service';
import { Router } from '@angular/router';
import { SpinnerService } from '../../shared/services/spinner/spinner.service';
import * as AuthActions from '../../state/auth/auth.actions';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass'],
})
export class LoginComponent implements OnInit, AfterViewInit {
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements: ElementRef[] = [];
  loginForm: FormGroup = new FormGroup('');

  displayMessages: { [key: string]: string } = {};
  private genericValidator: GenericValidator = new GenericValidator();

  constructor(
    private fb: FormBuilder,
    private store: Store,
    public spinnerService: SpinnerService
  ) {
    this.genericValidator = new GenericValidator();
  }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  ngAfterViewInit(): void {
    const controlBlurs: Observable<any>[] = this.formInputElements.map(
      (formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur')
    );

    merge(this.loginForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(800))
      .subscribe((value) => {
        this.displayMessages = this.genericValidator.processMessages(
          this.loginForm
        );
      });
  }

  async login() {
    if (this.loginForm.invalid) return;

    const credentials = {
      email: this.loginForm.get('email')?.value,
      password: this.loginForm.get('password')?.value,
    };

    this.store.dispatch(AuthActions.loginRequest({ credentials }));
  }
}
