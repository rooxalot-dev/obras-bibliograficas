import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import IAuthor from '../models/IAuthor';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {

  private apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  public saveAuthors(authorsNames: { name: string }[]): Observable<IAuthor[]> {
    const saveUrl = `${this.apiUrl}/authors`;
    const observable = this.http.post<IAuthor[]>(saveUrl, authorsNames);

    return observable;
  }

  public getAuthors(name: string = ''): Observable<IAuthor[]> {
    const saveUrl = `${this.apiUrl}/authors`;
    const observable = this.http.get<IAuthor[]>(saveUrl, {
      params: {
        name,
      }
    })

    return observable;
  }
}
