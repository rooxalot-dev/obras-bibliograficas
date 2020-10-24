import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from './modules/angular-material.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AuthorService } from './services/author.service';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { MainHeaderComponent } from './components/main-header/main-header.component';
import { SideMenuComponent } from './components/side-menu/side-menu.component';
import { HomeComponent } from './pages/home/home.component';
import { AuthorsComponent } from './pages/authors/authors.component';
import { AddAuthorDialogComponent } from './components/add-author-dialog/add-author-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    MainHeaderComponent,
    SideMenuComponent,
    HomeComponent,
    AuthorsComponent,
    AddAuthorDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    AngularMaterialModule,
    ToastrModule.forRoot(),
  ],
  providers: [
    AuthorService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
