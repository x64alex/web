import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent {
  book: any = {};

  constructor(private http: HttpClient) {}

  onSubmit() {
    this.http.post('http://localhost/web/hw2/updateBook.php', this.book).subscribe(
      data => {
        console.log('Success');
        console.log(data);
      },
      error => {
        console.log('Error');
        console.log(error);
      }
    );
  }
}
