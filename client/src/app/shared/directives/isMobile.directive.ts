import {
  Directive,
  OnInit,
  TemplateRef,
  ViewContainerRef,
} from '@angular/core';
import { DeviceDetectorService } from 'ngx-device-detector';
@Directive({
  selector: '[isMobile]',
})
export class IsMobileDirective implements OnInit {
  constructor(
    private deviceService: DeviceDetectorService,
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef
  ) {}

  ngOnInit() {
    if (this.deviceService.isMobile() || this.deviceService.isTablet()) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainer.clear();
    }
  }
}
