package com.gui.controllers;


import java.util.List;

import javax.ejb.EJB;
import javax.faces.bean.ManagedBean;

import com.ejb.services.InstrumentService;
import com.jpa.entities.Instrument;

@ManagedBean
public class InstrumentController {
	private Instrument instrument = new Instrument();
	private String naziv;
	private int studio;
	private double cena;
	private int id;
	
	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public double getCena() {
		return cena;
	}

	public void setCena(double cena) {
		this.cena = cena;
	}

	@EJB 
	private InstrumentService service;
	
	public String getNaziv() {
		return naziv;
	}

	public void setNaziv(String naziv) {
		this.naziv = naziv;
	}

	public int getStudio() {
		return studio;
	}

	public void setStudio(int studio) {
		this.studio = studio;
	}

	public Instrument getInstrument() {
		return instrument;
	}
	
	public void setInstrument(Instrument i) {
		instrument = i;
	}
	
	public void saveInstrument() {
		instrument = new Instrument(naziv, studio, cena);
		service.dodajInstrument(instrument);
	}
	public List<Instrument> allInstruments(){
		List<Instrument> lista = service.sviInstrumenti();
		
		return lista;
	}
	
	public void updateInstrument() {
		service.azurirajInstrument(id, naziv, studio, cena);
	}
	
	public void deleteInstrument() {
		service.ukloniInstrument(id);
	}
	
	public Instrument findInstrument() {
		this.instrument = service.nadjiInstrument(id);
		return instrument;
	}
	
}
