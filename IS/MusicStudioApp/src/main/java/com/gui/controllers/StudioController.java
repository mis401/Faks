package com.gui.controllers;

import java.util.ArrayList;
import java.util.List;

import javax.ejb.EJB;
import javax.faces.bean.ManagedBean;

import com.ejb.services.StudioService;
import com.jpa.entities.Studio;

@ManagedBean
public class StudioController {
	private Studio studio = new Studio();
	private String naziv;
	private int id;
	
	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	@EJB
	private StudioService service;

	public Studio getStudio() {
		return studio;
	}

	public void setStudio(Studio studio) {
		this.studio = studio;
	}

	public String getNaziv() {
		return naziv;
	}

	public void setNaziv(String naziv) {
		this.naziv = naziv;
	}
	
	public void saveStudio() {
		studio = new Studio(naziv);
		service.dodajStudio(studio);
	}
	
	public void deleteStudio(int id) {
		service.ukloniStudio(id);
	}
	
	public void updateStudio() {
		service.azurirajStudio(studio.getId(), naziv);
	}
	
	public Studio findStudio(int id) {
		return service.nadjiStudio(id);
	}
}
