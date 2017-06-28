<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:output omit-xml-declaration="yes" indent="yes"/>
  <xsl:template match="/">
    <xsl:for-each select="BazaFaktur/listaFaktur/Faktura">
    <Faktura>
    <numerFaktury>
    <xsl:value-of select="NumerFaktury"/>
    </numerFaktury>
    <dataWystawienia>
    <xsl:value-of select="DataWystawienia"/>
    </dataWystawienia>
    <OdbiorcaFizyczny>
    <xsl:value-of select="OdbiorcaFizyczny/Imie"/> <xsl:value-of select="OdbiorcaFizyczny/Nazwisko"/>
    </OdbiorcaFizyczny>
    <FizycznyRabat>
    <xsl:value-of select="OdbiorcaFizyczny/Rabat"/>
    </FizycznyRabat>
    <OdbiorcaPrawny>
    <xsl:value-of select="OdbiorcaPrawny/Nazwa"/>
    </OdbiorcaPrawny>
    <PrawnyRabat>
    <xsl:value-of select="OdbiorcaPrawny/Rabat"/>
    </PrawnyRabat>
    <Produkty>
    <xsl:for-each select="ProduktyList/KeyValuePairOfProduktInt32">
    <Produkt>
      <Nazwa>
      <xsl:value-of select="Key/Nazwa"/>
      </Nazwa>
      <CenaHurtownia>
      <xsl:value-of select="Key/CenaHurtownia"/>
      </CenaHurtownia>
      <CenaJednostkowa>
      <xsl:value-of select="Key/CenaJednostkowa"/>
      </CenaJednostkowa>
      <CenaBrutto>
      <xsl:value-of select="Key/CenaBrutto"/>
      </CenaBrutto>
      <Ilosc>
      <xsl:value-of select="Value"/>
      </Ilosc>
    </Produkt>
    </xsl:for-each>
    </Produkty>
    <Razem>
    <xsl:value-of select="Razem"/>
    </Razem>
    </Faktura>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>