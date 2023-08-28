import { Component, OnInit } from '@angular/core';
import { GenericHttpService } from '../../shared/services/http-service';
import { SpinnerService } from '../../shared/services/spinner/spinner.service';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass'],
})
export class DashboardComponent implements OnInit {
  dayPlans: any;
  constructor(
    private httpService: GenericHttpService,
    public spinnerService: SpinnerService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.httpService
      .getList(
        `DayPlan/${
          this.authService.userId.value ||
          '3b36ee63-2e1e-48d3-82a6-9e2fc6d41d57'
        }`
      )
      .subscribe((res) => (this.dayPlans = res));
  }

  getDayPlans(res: Response) {}
}
