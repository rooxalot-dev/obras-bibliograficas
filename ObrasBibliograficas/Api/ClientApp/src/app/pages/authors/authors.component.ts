import { Component, OnInit } from '@angular/core';
import IAuthor from 'src/app/models/IAuthor';
import { MatDialog } from '@angular/material/dialog';
import { AddAuthorDialogComponent } from 'src/app/components/add-author-dialog/add-author-dialog.component';
import { AuthorService } from 'src/app/services/author.service';
import { ToastrService } from 'ngx-toastr';
import { IError } from '../../models/IError';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.scss']
})
export class AuthorsComponent implements OnInit {

  authorsList: IAuthor[] = [];

  authorsListColunns: string[] = ['name', 'formattedName'];

  constructor(
    public authorsService: AuthorService,
    public toastr: ToastrService,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getAuthors();
  }

  getAuthors(): void {
    const options = {
      year: 'numeric', month: 'numeric', day: 'numeric',
      hour: 'numeric', minute: 'numeric', second: 'numeric',
      hour12: false,
      timeZone: 'UTC',
    };
    const intlDateFormatter = new Intl.DateTimeFormat('pt-BR', options);

    this.authorsService.getAuthors().subscribe((data) => {
      console.log('data', data);
      this.authorsList = data;

      this.authorsList = this.authorsList.map((author) => ({
        ...author,
        // formattedCreatedAt: author.createdAt ? intlDateFormatter.format(author.createdAt) : '--',
        // formattedUpdatedAt: author.updatedAt ? intlDateFormatter.format(author.updatedAt) : '--',
      }));
    },
      (error: IError) => this.toastr.error(error.detailed, 'Não foi possível salvar os dados.'));
  }

  hasAuthors(): boolean {
    return this.authorsList.length > 0;
  }

  openDialog() {
    const dialogRef = this.dialog.open(AddAuthorDialogComponent, {
      disableClose: true,
      height: '80vh',
    });

    dialogRef.afterClosed().subscribe(result => {
      const { cancel } = result;

      if (cancel && cancel === true) {
        console.log('CLOSED');
      } else {
        this.authorsService.saveAuthors(result).subscribe(
          (data) => {
            this.toastr.success('Autores salvos com sucesso!')
            this.getAuthors();
          },
          (error: IError) => this.toastr.error(error.detailed, 'Não foi possível salvar os dados.'),
        )
      }
    });
  }
}
