import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GastoService } from '../../servicios/gasto.service';

@Component({
  selector: 'app-gasto-detail',
  standalone: true,
  templateUrl: './gasto-detail.component.html'
})

export class GastoDetailComponent {

  gasto: any;

  constructor(
    private route: ActivatedRoute,
    private gastoService: GastoService
  ) {}


  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    console.log(id);
  }
}
 