import { Component, OnInit } from '@angular/core';
import template from '../../../contract-template'
import { ContractService } from '../contract/contract.service';
import * as FileSaver from "file-saver";

@Component({
  selector: 'app-contract-template',
  templateUrl: './contract-template.component.html',
  styleUrls: ['./contract-template.component.scss']
})
export class ContractTemplateComponent implements OnInit {

  template = template;

  constructor( private contractServices: ContractService) { }

  ngOnInit(): void {
  }

  downTemplate() {
    this.contractServices
      .downRootFile()
      .subscribe((response: any) => {
        const data: Blob = new Blob([response]);
        FileSaver.saveAs(data, "mau-hop-dong.doc");
      });
  }
}
