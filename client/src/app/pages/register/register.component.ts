import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChildren,
} from '@angular/core';
import {
  FormBuilder,
  FormControlName,
  FormGroup,
  Validators,
} from '@angular/forms';
import { PasswordMatcher } from '../../shared/password-matcher';
import { GenericValidator } from '../../shared/generic-validator';
import { Observable, debounceTime, fromEvent, merge } from 'rxjs';
import { AuthService } from '../../auth/auth.service';
import { Router } from '@angular/router';
import { SpinnerService } from '../../shared/services/spinner/spinner.service';
import { Store } from '@ngrx/store';
import * as AuthActions from '../../state/auth/auth.actions';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.sass'],
})
export class RegisterComponent implements OnInit, AfterViewInit {
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements: ElementRef[] = [];
  registerForm: FormGroup = new FormGroup('');

  displayMessages: { [key: string]: string } = {};
  private genericValidator: GenericValidator = new GenericValidator();

  constructor(
    private fb: FormBuilder,
    public spinnerService: SpinnerService,
    private store: Store
  ) {
    this.genericValidator = new GenericValidator();
  }

  ngOnInit(): void {
    this.registerForm = this.fb.group(
      {
        fName: ['', [Validators.required]],
        lName: ['', [Validators.required]],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength]],
        confirm_password: ['', [Validators.required]],
      },
      { validator: PasswordMatcher.match }
    );
  }

  ngAfterViewInit(): void {
    const controlBlurs: Observable<any>[] = this.formInputElements.map(
      (formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur')
    );

    merge(this.registerForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(800))
      .subscribe(() => {
        this.displayMessages = this.genericValidator.processMessages(
          this.registerForm
        );
      });
  }

  register() {
    if (this.registerForm.invalid) return;

    this.store.dispatch(
      AuthActions.registerRequest({ credentials: this.registerForm.value })
    );
  }
}
