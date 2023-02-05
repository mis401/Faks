package com.jpa.entities;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.persistence.TableGenerator;

@Entity
@Table(name = "zahtevi")
public class Zahtev {
	/*
	 * @TableGenerator( name = "zahtevi_gen", table = "generatori", pkColumnName =
	 * "ime_generatora", valueColumnName = "vrednost_generatora", allocationSize =
	 * 1, pkColumnValue = "zahtevi_gen" )
	 */
	@Id
	@GeneratedValue(
			strategy = GenerationType.IDENTITY
			//generator = "zahtevi_gen"
	)
	
	@Column(name = "id", updatable = false, nullable = false)
	private int id;
	
	
	@Column(name = "vreme", nullable = false)
	private Date vreme;
	
	@Column(name = "studio")
	private int studioId;
	
	@Column(name = "sala")
	private int salaId;
	
	@Column(name = "usluge")
	private Boolean usluge;
	
	@Column(name = "broj_pesama")
	private int brojPesama;
	
	@Column(name = "cena")
	private double cena;
	
	
	public Zahtev() {}
	
	public Zahtev(Date vreme, int studioId, int salaId, int brojPesama, double cena, Boolean usluge) {
		this.vreme = vreme;
		this.cena = cena;
		this.studioId = studioId;
		this.salaId = salaId;
		this.brojPesama = brojPesama;
		this.usluge = usluge;
	}
	
	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public Date getVreme() {
		return vreme;
	}

	public void setVreme(Date vreme) {
		this.vreme = vreme;
	}

	public int getSalaId() {
		return salaId;
	}

	public void setSalaId(int salaId) {
		this.salaId = salaId;
	}

	public Boolean getUsluge() {
		return usluge;
	}

	public void setUsluge(Boolean usluge) {
		this.usluge = usluge;
	}

	public int getBrojPesama() {
		return brojPesama;
	}

	public void setBrojPesama(int brojPesama) {
		this.brojPesama = brojPesama;
	}
	
	public double getCena() {
		return cena;
	}
	
	public void setCena(double cena) {
		this.cena = cena;
	}
	


	
}
