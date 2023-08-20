import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.sass'],
})
export class ButtonComponent {
  @Input() color:
    | 'primary'
    | 'secondary'
    | 'tetiary'
    | 'danger'
    | 'success'
    | 'outline'
    | string = 'primary';
  @Input() text: string = 'Click';
  @Output() btnClick = new EventEmitter();

  constructor() {}

  ngOnInit(): void {}

  onClick(): void {
    this.btnClick.emit();
  }
}
