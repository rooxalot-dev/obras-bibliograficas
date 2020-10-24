import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormGroup, FormControl, FormArray } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-author-dialog',
  templateUrl: './add-author-dialog.component.html',
  styleUrls: ['./add-author-dialog.component.scss']
})
export class AddAuthorDialogComponent implements OnInit {
  @ViewChild('stepper') stepper: MatStepper;
  authorsCount: number = 0;
  authorForm: FormArray = new FormArray([]);

  constructor(
    private dialogRef: MatDialogRef<AddAuthorDialogComponent>,
    private toastr: ToastrService) {}

  ngOnInit(): void {}

  close() {
    this.dialogRef.close({ cancel: true });
  }

  nextStep() {
    const stepIndex = this.stepper.selectedIndex;

    switch (stepIndex) {
      case 0:
          if (!this.authorsCount || this.authorsCount === 0) {
            this.toastr.warning('Especifique quantos autores serão cadastrados!');
            return;
          }

        this.authorForm.clear();

        for (let index = 0; index < this.authorsCount; index++) {
          this.authorForm.push(new FormControl(''));
        }

        this.stepper.next();
        break;
      case 1:
          const authorsValue = this.authorForm.value as { [key: string]: string };
          const hasEmptyValues = Object.values(authorsValue).some((value) => !value);

          if (hasEmptyValues) {
            this.toastr.warning('Preencha o nome de todos os autores!');
            return;
          }

        this.stepper.next();
        break;
      case 2:
        const formValue = this.authorForm.value as { [key: string]: string };
        const authors = Object.values(formValue).map(value => ({
          name: value
        }));

        this.dialogRef.close(authors);
        break;
      default:
        this.stepper.next();
        break;
    }
  }

  previousStep() {
    const stepIndex = this.stepper.selectedIndex;

    if (stepIndex > 0) {
      this.stepper.previous();
    }
  }
}
