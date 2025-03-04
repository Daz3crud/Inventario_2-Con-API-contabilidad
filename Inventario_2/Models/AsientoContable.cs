﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario_2.Models
{
    public class AsientoContable
    {
        public string? Descripcion { get; set; }
        public int Auxiliar { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }

        // Listas de transacciones
        public List<Transaccion> Transacciones { get; set; }

        // Referencias a las otras entidades
        public Estado? Estado { get; set; }
        public Moneda? Moneda { get; set; }
        public int IdMovimiento { get; internal set; }
        public int CuentaCr { get; internal set; }
        public int CuentaDb { get; internal set; }
    }

    public class Transaccion
    {
        public decimal Monto { get; set; }

        // Referencias a las otras entidades
        public Cuenta? Cuenta { get; set; }
        public TipoMovimiento? TipoMovimiento { get; set; }
    }

    public class Estado
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
    }

    public class Moneda
    {
        public int Id { get; set; }
        public string? CodigoIso { get; set; }
        public string? Descripcion { get; set; }
        public decimal TasaCambio { get; set; }
    }

    public class TipoMovimiento
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
    }

    public class Cuenta
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public int Tipo { get; set; }
    }


}


