import { Component, OnInit, Input, ViewChild, AfterViewInit } from '@angular/core';

import { menuItems } from '../../../menu';
import { Observable } from 'rxjs';
import { MatDrawer } from '@angular/material/sidenav';

@Component({
  selector: 'app-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss']
})
export class SideMenuComponent implements OnInit, AfterViewInit {
  @Input() menuObservable: Observable<boolean>;
  @ViewChild('drawer') menuDrawer: MatDrawer;
  links = menuItems;

  constructor() { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.menuObservable.subscribe((data) => {
      this.menuDrawer.toggle();
    });
  }
}
