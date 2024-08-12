import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DocumentIconComponent } from './document-icon.component';
import { DocumentDocument } from '../../services/generated';
import { Document } from '../../graphql/generated';
import { ComponentRef } from '@angular/core';

describe('DocumentIconComponent', () => {
  let component: DocumentIconComponent;
  let conponentRef: ComponentRef<DocumentIconComponent>;
  let fixture: ComponentFixture<DocumentIconComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocumentIconComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentIconComponent);
    component = fixture.componentInstance;
    conponentRef = fixture.componentRef;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should match the snapshot with no document provided', () => {
    expect(fixture).toMatchSnapshot();
  });

  it('should match the snapshot with a pdf document provided', () => {
    conponentRef.setInput('document', <Document>{ extension: '.pdf' });
    fixture.detectChanges();
    expect(fixture).toMatchSnapshot();
  });
});
