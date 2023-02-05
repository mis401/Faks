package com.jpa.entities;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.persistence.TableGenerator;

@Entity
@Table(name = "studio")
public class Studio {
	/*
	 * @TableGenerator( name = "studio_gen", table = "generatori", pkColumnName =
	 * "ime_generatora", valueColumnName = "vrednost_generatora", allocationSize =
	 * 1, pkColumnValue = "studio_gen" )
	 */
	@Id
	@GeneratedValue(
			strategy = GenerationType.IDENTITY
			//generator = "studio_gen"
	)
	
	@Column(name = "id", updatable = false, nullable = false)
	private int id;
	
	@Column(name = "naziv", nullable = false)
	private String naziv;
	
	public Studio() {}
	
	public Studio(String naziv) {
		this.naziv = naziv;
	}
	
	public String getNaziv() {return this.naziv;}
	public void setNaziv(String s) {this.naziv = s;}
	
	public int getId() {return this.id;}
	public void setId(int id) {this.id=id;}
	
	
}	
