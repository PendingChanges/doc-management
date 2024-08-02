import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { DocumentUploadFormComponent } from './document-upload-form.component';

@Component({
  selector: 'app-document-upload-button',
  standalone: true,
  imports: [
    ButtonModule,
    DialogModule,
    InputTextModule,
    DocumentUploadFormComponent,
  ],
  template: `<p-button (onClick)="showDialog()" label="Upload Document" />
    <p-dialog
      header="Upload Document"
      [modal]="true"
      [(visible)]="visible"
      [style]="{ width: '25rem' }"
    >
      <app-document-upload-form
        [(visible)]="visible"
      ></app-document-upload-form>
    </p-dialog>`,
  styles: '',
})
export class DocumentUploadButtonComponent {
  uploadedFiles: any[] = [];

  visible: boolean = false;

  showDialog() {
    this.visible = true;
  }
}
