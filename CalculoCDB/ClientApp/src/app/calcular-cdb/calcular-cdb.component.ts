import { Component, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Data } from 'popper.js';

@Component({
  selector: 'app-calcular-cdb',
  templateUrl: './calcular-cdb.component.html'
})
export class CalcularCdbComponent {
  public baseUrl: string = '';
  public erro: string = '';
  public calculando: boolean = false;
  public deuCertoCalculo: boolean = true;
  public resultadoCalculo!: ResultadoCalculo;
  public calculoForm: FormGroup;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseurl: string,
              private fb: FormBuilder) {
    this.baseUrl = baseurl;

    this.calculoForm = this.fb.group({
      valor: [0, Validators.required],
      meses: [0, Validators.required]
    });
  }

  calcular(): void {
    this.calculando = true;

    let params = new HttpParams();
    params = params.append('meses', this.calculoForm.get('meses')!.value);
    params = params.append('valor', this.calculoForm.get('valor')!.value);

    this.http.get<ResultadoCalculo>(this.baseUrl + 'calcular-cdb', { params }).subscribe(result => {
      this.calculando = false;
      this.deuCertoCalculo = true;
      this.resultadoCalculo = result;
    }, error => {
      this.calculando = false;
      this.deuCertoCalculo = false;
      console.log(error);
      this.erro = error;
    });
  }
}

interface ResultadoCalculo {
  valorInicial: number;
  dataRetirada: string;
  porcentagem: number;
  valorBruto: number;
  valorLiquido: number;
}
