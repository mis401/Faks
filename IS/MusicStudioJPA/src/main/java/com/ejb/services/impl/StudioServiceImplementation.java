package com.ejb.services.impl;

import com.ejb.services.StudioService;
import com.jpa.entities.Studio;
import com.jpa.entities.Instrument;
import com.jpa.entities.Sala;
import java.util.List;
import java.util.ArrayList;
import java.util.HashSet;
import javax.ejb.Stateless;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;
import javax.persistence.PersistenceContext;

public class StudioServiceImplementation implements StudioService {

	private EntityManager em;
	
	public StudioServiceImplementation() {
		EntityManagerFactory emf = Persistence.createEntityManagerFactory("MusicStudioPers");
		em = emf.createEntityManager();
	}
	
	@Override
	public void dodajStudio(Studio s) {
		try {
		em.getTransaction().begin();
		em.persist(s);
		em.getTransaction().commit();
		}
		catch(Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	public void ukloniStudio(int id) {
		try {
			Studio studio = em.find(Studio.class, id);
			if (studio == null) {
				throw new Exception("Ne postoji studio s tim id");
			}
			List<Instrument> instrumenti = em
					.createQuery("SELECT i FROM Instrument i WHERE i.studioId = :id", Instrument.class)
					.setParameter("id", id).getResultList();
			List<Sala> sale = em
					.createQuery("SELECT s FROM Sala s WHERE s.studioId = :id", Sala.class)
					.setParameter("id", id).getResultList();
			
			
			em.getTransaction().begin();
			
			for(Instrument i : instrumenti) {
				em.remove(i);
			}
			for(Sala s : sale) {
				em.remove(s);
			}
			em.remove(studio);
			
			em.getTransaction().commit();
			
		}
		catch(Exception e) {
			e.printStackTrace();
		}

	}

	@Override
	public void azurirajStudio(int id, String naziv) {
		try {
			Studio studio = em.find(Studio.class, id);
			if (studio == null) {
				throw new Exception("Ne postoji studio s tim id");
			}
			em.getTransaction().begin();
			studio.setNaziv(naziv);
			em.getTransaction().commit();
		}
		catch(Exception e) {
			e.printStackTrace();
		}

	}

	@Override
	public Studio nadjiStudio(int id) {
		try {
			Studio studio = em.find(Studio.class, id);
			if (studio == null) {
				throw new Exception("Ne postoji studio s tim id");
			}
			return studio;
		}
		catch(Exception e) {
			e.printStackTrace();
		}

		return null;
	}
	@Override
	public void ocistiTabeluStudio() {
		List<Studio> lista = em.createQuery("SELECT s FROM Studio s", Studio.class).getResultList();
		
		for(Studio s:lista) {
			ukloniStudio(s.getId());
		}
	}
}
