import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';

export interface RTreeNode {
  title: string;
  id: string;
  Parent: RTreeNode;
}

@Injectable({
  providedIn: 'root'
})
export class ServerService {

  constructor(private @Inject('BASE_URL') baseUrl: string) { }

  
}
