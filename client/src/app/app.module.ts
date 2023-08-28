import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { FormComponent } from './form/form.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { FooterComponent } from './components/footer/footer.component';
import { ButtonComponent } from './components/button/button.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { SideNavComponent } from './components/side-nav/side-nav.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { IsMobileDirective } from './shared/directives/isMobile.directive';
import { InterceptorService } from './shared/services/http-interceptors/http.interceptor.service';
import { SpinnerComponent } from './shared/components/spinner/spinner.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DashboardComponent,
    RegisterComponent,
    LoginComponent,
    FormComponent,
    NavbarComponent,
    FooterComponent,
    ButtonComponent,
    SideNavComponent,
    IsMobileDirective,
    SpinnerComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem('token'),
      },
    }),
    FontAwesomeModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
