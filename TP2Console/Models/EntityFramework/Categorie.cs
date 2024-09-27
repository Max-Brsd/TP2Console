using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TP2Console.Models.EntityFramework;

[Table("categorie")]
public partial class Categorie
{
    [Key]
    [Column("idcategorie")]
    public int Idcategorie { get; set; }

    [Column("nom")]
    [StringLength(50)]
    public string Nom { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [InverseProperty(nameof(Film.IdcategorieNavigation))]
    public virtual ICollection<Film> Films { get; set; } = new List<Film>();

    /*[InverseProperty(nameof(Film.IdcategorieNavigation))]
    private ICollection<Film> _films;

        [InverseProperty(nameof(Film.IdcategorieNavigation))]
        public virtual ICollection<Film> Films
        {
            get
            {
                return _lazyLoader?.Load(this, ref _films) ?? _films;
            }
            set
            {
                _films = value;
            }
        }
    

    private ILazyLoader _lazyLoader;
    public Categorie(ILazyLoader lazyLoader)
    {
        _lazyLoader = lazyLoader;
    }*/
}
