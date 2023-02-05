package com.jpa.entities;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.persistence.TableGenerator;

@Entity
@Table(name = "sale")
public class Sala {
	/*
	 * @TableGenerator( name = "sale_gen", table = "generatori", pkColumnName =
	 * "ime_generatora", valueColumnName = "vrednost_generatora", allocationSize =
	 * 1, pkColumnValue = "sale_gen" )
	 */
	@Id
	@GeneratedValue(
			strategy = GenerationType.IDENTITY
			//generator = "sale_gen"
	)
	
	@Column(name = "id", updatable = false, nullable = false)
	private int id;

	@Column(name = "broj_sale", nullable = false)
	private int brojSale;
	
	@Column(name = "cena", nullable = false)
	private double cena;
	
	@Column(name = "studio")
	private int studioId;
	
	public Sala() {}
	public Sala(int broj, int studio, double cena) {
		this.brojSale = broj;
		this.cena = cena;
		this.studioId = studio;
	}
	
	public double getCena() {
		return cena;
	}
	public void setCena(double cena) {
		this.cena = cena;
	}
	public int getId() {return this.id;}
	public void setId(int id) {this.id = id;}
	
	public int getBrojSale() {return this.brojSale;}
	public void setBrojSale(int br) {this.brojSale = br;}
	
	public int getStudioId() {return this.studioId;}
	public void setStudioId(int id) {this.studioId = id;}
	
	
	
}
	