import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-main-header',
  templateUrl: './main-header.component.html',
  styleUrls: ['./main-header.component.scss']
})
export class MainHeaderComponent implements OnInit {
  headerTitle: string;
  @Output() toogleClick = new EventEmitter<boolean>();

  constructor(private title: Title) {
    this.headerTitle = this.title.getTitle();
  }

  ngOnInit(): void {
  }

  onToogleClick() {
    this.toogleClick.emit(true);
  }

}
