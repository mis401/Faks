package com.gui.controllers;


import java.util.List;

import javax.ejb.EJB;
import javax.faces.bean.ManagedBean;

import com.ejb.services.SalaService;
import com.jpa.entities.Sala;

@ManagedBean
public class SalaController {
	private Sala sala = new Sala();
	private int brojSale;
	private int studioId;
	private double cena;
	private int id;
	private List<Sala> list;
	
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public int getBrojSale() {
		return brojSale;
	}
	public void setBrojSale(int brojSale) {
		this.brojSale = brojSale;
	}
	public int getStudioId() {
		return studioId;
	}
	public void setStudioId(int studioId) {
		this.studioId = studioId;
	}
	public double getCena() {
		return cena;
	}
	public void setCena(double cena) {
		this.cena = cena;
	}
	
	@EJB
	private SalaService service;
	
	public void saveSala() {
		sala = new Sala(brojSale, studioId, cena);
		service.dodajSalu(sala);
	}
	
	public void deleteSala(int id) {
		service.ukloniSalu(id);
	}
	
	public Sala findSala(int id) {
		return service.nadjiSalu(id);
	}
	
	public List<Sala> allSala(){
		list = service.sveSale();
		return list;
	}
	
	public void updateSala() {
		service.azurirajSalu(id, brojSale, studioId, cena);
	}
	
	
	
}
