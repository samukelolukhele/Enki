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
import { GenericValidator } from '../shared/generic-validator';
import { Observable, debounceTime, fromEvent, merge } from 'rxjs';
import { GenericHttpClient } from '../shared/generic-http-client';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { User } from '../types/User.type';

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

  constructor(private fb: FormBuilder, private http: GenericHttpClient) {
    this.genericValidator = new GenericValidator();
  }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
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

  login() {
    return this.http.getList<User>('User').subscribe((res) => console.log(res));
  }
}
