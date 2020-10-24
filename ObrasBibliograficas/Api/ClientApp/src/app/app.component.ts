import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Obras Bibliográficas';
  menuSubject$ = new Subject();

  constructor (private titleService: Title) {
    titleService.setTitle(this.title);
  }

  toogleMenu() {
    this.menuSubject$.next(true);
  }
}
