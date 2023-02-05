package com.jpa.entities;


import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.persistence.TableGenerator;

@Entity
@Table(name = "zahtevi_instrumenti")
public class ZahtevInstrument {
	/*
	 * @TableGenerator( name = "zahtevi_instrumenti_gen", table = "generatori",
	 * pkColumnName = "ime_generatora", valueColumnName = "vrednost_generatora",
	 * allocationSize = 1, pkColumnValue = "zahtevi_instrumenti_gen" )
	 */
	@Id
	@GeneratedValue(
			strategy = GenerationType.IDENTITY
			//generator = "zahtevi_instrumenti_gen"
	)
	
	@Column(name = "id", updatable = false, nullable = false)
	private int id;
	
	@Column(name = "instrument", nullable = false)
	private int instrumentId;
	
	@Column(name = "zahtev", nullable = false)
	private int zahtevId;
	
	
	public ZahtevInstrument() {}
	public ZahtevInstrument(int iid, int zid) {
		this.instrumentId = iid;
		this.zahtevId = zid;
	}
	
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public int getInstrumentId() {
		return instrumentId;
	}
	public void setInstrumentId(int instrumentId) {
		this.instrumentId = instrumentId;
	}
	public int getZahtevId() {
		return zahtevId;
	}
	public void setZahtevId(int zahtevId) {
		this.zahtevId = zahtevId;
	}
	
}
