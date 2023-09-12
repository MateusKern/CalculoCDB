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
  public erros: string[] = [];
  public calculando: boolean = false;
  public deuCertoCalculo: boolean = true;
  public resultadoCalculo!: ResultadoCalculo;
  public calculoForm: FormGroup;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseurl: string,
              private fb: FormBuilder) {
    this.baseUrl = baseurl;

    this.calculoForm = this.fb.group({
      valor: [0],
      meses: [0]
    });
  }

  calcular(): void {
    this.calculando = true;
    this.erros = [];

    let params = new HttpParams();
    params = params.append('meses', Math.trunc(this.calculoForm.get('meses')!.value ?? 0));
    params = params.append('valor', this.calculoForm.get('valor')!.value ?? 0);

    this.http.get<ResultadoCalculo>(this.baseUrl + 'calcular-cdb', { params }).subscribe({
      next: (result) => {
        this.calculando = false;
        this.deuCertoCalculo = true;
        this.resultadoCalculo = result;
      },
      error: (error) => {
        this.calculando = false;
        this.deuCertoCalculo = false;
        console.log(error);
        this.erros = error.error;
      }
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
