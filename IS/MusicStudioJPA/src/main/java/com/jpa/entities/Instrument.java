package com.jpa.entities;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.persistence.TableGenerator;

@Entity
@Table(name = "instrumenti")
public class Instrument {
	/*
	 * @TableGenerator( name = "instrumenti_gen", table = "generatori", pkColumnName
	 * = "ime_generatora", valueColumnName = "vrednost_generatora", allocationSize =
	 * 1, pkColumnValue = "instrumenti_gen" )
	 */
	@Id
	@GeneratedValue(
			strategy = GenerationType.IDENTITY
			//generator = "instrumenti_gen"
	)
	
	@Column(name = "id", updatable = false, nullable = false)
	private int id;

	@Column(name = "naziv", nullable = false)
	private String naziv;
	
	@Column(name = "cena", nullable = false)
	private double cena;
	
	@Column(name = "studio")
	private int studioId;
	
	public Instrument() {}
	public Instrument(String naziv, int sid, double cena) {
		this.naziv = naziv;
		this.cena = cena;
		this.studioId = sid;
	}
	
	public double getCena() {
		return cena;
	}
	public void setCena(double cena) {
		this.cena = cena;
	}
	public int getId() {
		return this.id;
	}
	public void setId(int id) {
		this.id = id;
	}
	
	
	public String getNaziv() {
		return this.naziv;
	}
	
	public void setNaziv(String s) {
		this.naziv = s;
	}
	
	public int getStudioId() {return this.studioId;}
	public void setStudioId(int id) {this.studioId = id;}
	
}
