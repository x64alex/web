import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { map } from 'rxjs/operators';

//import { Book } from 'Book.ts';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  baseUrl = 'http://localhost/web/hw2';

  constructor(private http: HttpClient) {}

  getAll(category: string) {
    const data = {
      category: category
    };
  
    return this.http.post(`${this.baseUrl}/browse.php`, data).pipe(
      map((res: any) => {
        return res['data'];
      })
    );
  }
}
