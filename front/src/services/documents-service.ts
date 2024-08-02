import { Injectable } from '@angular/core';
import { ApolloQueryResult } from '@apollo/client/core';
import {
  AllDocumentsCollectionSegment,
  AllDocumentsGQL,
  AllDocumentsQuery,
  AllDocumentsQueryVariables,
} from '../graphql/generated';
import { map, Observable } from 'rxjs';
import { QueryRef } from 'apollo-angular';

@Injectable({
  providedIn: 'root',
})
export class DocumentsService {
  private _allDocumentsQueryRef: QueryRef<
    AllDocumentsQuery,
    AllDocumentsQueryVariables
  >;

  public allDocuments$: Observable<AllDocumentsCollectionSegment>;

  constructor(private _docuemntsGQL: AllDocumentsGQL) {
    this._allDocumentsQueryRef = this._docuemntsGQL.watch({
      skip: 0,
      take: 15,
    });

    this.allDocuments$ = this._allDocumentsQueryRef.valueChanges.pipe(
      map((result) => <AllDocumentsCollectionSegment>result.data.allDocuments)
    );
  }

  public refreshDocuments(skip: number, take: number): void {
    this._allDocumentsQueryRef.refetch({
      skip: skip,
      take: take,
    });
  }
}
