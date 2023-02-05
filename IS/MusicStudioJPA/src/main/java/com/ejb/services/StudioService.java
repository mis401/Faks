package com.ejb.services;

import com.jpa.entities.Studio;

public interface StudioService {
	public void dodajStudio(Studio s);
	public void ukloniStudio(int id);
	public void azurirajStudio(int id, String naziv);
	public Studio nadjiStudio(int id);
	public void ocistiTabeluStudio();
}
