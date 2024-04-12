grammar NucleicAcid;

nucleicAcid : dna | rna ;

dna : dnaTripletCode+ ;

dnaTripletCode : dnaBase dnaBase dnaBase ;

dnaBase : ADENINE | CYTOSINE | GUANINE | THYMINE | DELETE ;

rna : rnaTripletCode+ ;

rnaTripletCode : rnaBase rnaBase rnaBase ;

rnaBase : ADENINE | CYTOSINE | GUANINE | URACIL | DELETE ;

ADENINE : 'A' ;
CYTOSINE : 'C' ;
GUANINE : 'G' ;
THYMINE : 'T' ;
URACIL : 'U' ;
DELETE : '-' ;

WS : [\t\r\n]+ -> skip ;
