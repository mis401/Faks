package com;


import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotEquals;
import static org.junit.Assert.assertNotNull;
import static org.junit.Assert.assertSame;
import static org.junit.Assert.assertTrue;
import static org.junit.Assert.assertFalse;

import java.util.List;
import java.util.ArrayList;
import java.util.Date;

import com.ejb.services.SalaService;
import com.ejb.services.InstrumentService;
import com.ejb.services.ZahtevService;
import com.ejb.services.StudioService;
import com.ejb.services.impl.SalaServiceImplementation;
import com.ejb.services.impl.InstrumentServiceImplementation;
import com.ejb.services.impl.ZahtevServiceImplementation;
import com.ejb.services.impl.StudioServiceImplementation;
import com.jpa.entities.Studio;
import com.jpa.entities.Sala;
import com.jpa.entities.Instrument;
import com.jpa.entities.Zahtev;
import com.jpa.entities.ZahtevInstrument;


// import org.hibernate.event.spi.PreCollectionRecreateEvent;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;

public class GlobalTest {
	private static SalaService salaService;
	private static ZahtevService zahtevService;
	private static StudioService studioService;
	private static InstrumentService instrumentService;
	
	public GlobalTest() {
		salaService = new SalaServiceImplementation();
		zahtevService = new ZahtevServiceImplementation();
		studioService = new StudioServiceImplementation();
		instrumentService = new InstrumentServiceImplementation();
	}
	
	@Before
	public void initTest() {
		studioService.dodajStudio(new Studio("Test studio"));
		Instrument truba = new Instrument("truba", 1, 1500);
		Instrument klavir = new Instrument("klavir", 1, 20000);
		instrumentService.dodajInstrument(truba);
		instrumentService.dodajInstrument(klavir);
		salaService.dodajSalu(new Sala(2, 1, 500));
		Date datum = new Date();
		List<Integer> lista = new ArrayList<>();
		lista.add(truba.getId());
		lista.add(klavir.getId());
		System.out.println("IDevi instrumenata su:");
		for(int id : lista) {
			System.out.println(id);
		}
		zahtevService.dodajZahtev(new Zahtev(datum, 1, 1, 1, 1500, false), lista);
	}
	
	@After
	public void postTest() {
		studioService.ocistiTabeluStudio();
		zahtevService.ocistiTabeluZahtev();
	}
	
	@Test
	public void test() {
		try {
		List<Instrument> instrumenti = instrumentService.sviInstrumenti();
		assertNotNull(instrumenti);
		assertFalse(instrumenti.isEmpty());
		List<Instrument> instrumentiUZahtevu = zahtevService.nadjiInstrumente(zahtevService.sviZahtevi().get(0).getId());
		assertNotNull(instrumentiUZahtevu);
		assertFalse(instrumentiUZahtevu.isEmpty());
		assertTrue(instrumentiUZahtevu.get(0).getNaziv().equals("truba"));
		assertTrue(instrumentiUZahtevu.get(1).getNaziv().equals("klavir"));
		System.out.println(instrumentiUZahtevu.get(0).getNaziv());
		}
		catch(Exception e) {
			e.printStackTrace();
		}
	}
}
