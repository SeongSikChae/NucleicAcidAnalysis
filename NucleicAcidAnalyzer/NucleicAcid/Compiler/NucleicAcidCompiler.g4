grammar NucleicAcidCompiler;

nucleicAcid : dna | rna ;

dna : dnaCode+ ;

rna : rnaCode+ ;

dnaCode : dnaBase dnaBase dnaBase ;

dnaBase : ADENINE | CYTOSINE | GUANINE | THYMINE | DELETE ;

rnaCode : rnaBase rnaBase rnaBase ;

rnaBase : ADENINE | CYTOSINE | GUANINE | URACIL | DELETE ;

ADENINE : 'A' ;
CYTOSINE : 'C' ;
GUANINE : 'G' ;
THYMINE : 'T' ;
URACIL : 'U' ;
DELETE : '-' ;