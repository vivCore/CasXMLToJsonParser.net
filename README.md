# CasXMLToJsonParser.net
Base version

This .net console application will convert cTakes Clinical Pipeline generated XML into a Json File. Generated output will have sample output as below:

[{
    "ClinicalTerm": "MRI",
    "Mentions": [
      [
        {
          "Id": 14551,
          "MentionName": "ProcedureMention",
          "RefSofa": 3,
          "Begin": 526,
          "End": 529,
          "RefOntologyConceptArr": 14547,
          "TypeID": 5,
          "UmlsConcepts": [
            {
              "Id": 14537,
              "Code": 312250003,
              "CodingScheme": "SNOMEDCT_US",
              "Score": 0,
              "CUI": "C0024485",
              "TUI": "T060",
              "PreferredText": "Magnetic Resonance Imaging"
            },
            {
              "Id": 14527,
              "Code": 113091000,
              "CodingScheme": "SNOMEDCT_US",
              "Score": 0,
              "CUI": "C0024485",
              "TUI": "T060",
              "PreferredText": "Magnetic Resonance Imaging"
            }
          ]
        }
      ]
    ]
  },
   {
    "ClinicalTerm": "Tumor",
    "Mentions": [
      [
        {
          "Id": 12645,
          "MentionName": "DiseaseDisorderMention",
          "RefSofa": 3,
          "Begin": 704,
          "End": 709,
          "RefOntologyConceptArr": 12642,
          "TypeID": 2,
          "UmlsConcepts": [
            {
              "Id": 12632,
              "Code": 108369006,
              "CodingScheme": "SNOMEDCT_US",
              "Score": 0,
              "CUI": "C0027651",
              "TUI": "T191",
              "PreferredText": "Neoplasms"
            }
          ]
        }
      ],
      [
        {
          "Id": 12689,
          "MentionName": "DiseaseDisorderMention",
          "RefSofa": 3,
          "Begin": 845,
          "End": 850,
          "RefOntologyConceptArr": 12686,
          "TypeID": 2,
          "UmlsConcepts": [
            {
              "Id": 12676,
              "Code": 108369006,
              "CodingScheme": "SNOMEDCT_US",
              "Score": 0,
              "CUI": "C0027651",
              "TUI": "T191",
              "PreferredText": "Neoplasms"
            }
          ]
        }
      ]
    ]
  },
  {
    "ClinicalTerm": "ANTERIOR",
    "Mentions": [
      [
        {
          "Id": 12821,
          "MentionName": "DiseaseDisorderMention",
          "RefSofa": 3,
          "Begin": 1870,
          "End": 1878,
          "RefOntologyConceptArr": 12818,
          "TypeID": 2,
          "UmlsConcepts": [
            {
              "Id": 12808,
              "Code": 51742006,
              "CodingScheme": "SNOMEDCT_US",
              "Score": 0,
              "CUI": "C0751437",
              "TUI": "T047",
              "PreferredText": "Adenohypophyseal Diseases"
            }
          ]
        }
      ],
      [
        {
          "Id": 12777,
          "MentionName": "DiseaseDisorderMention",
          "RefSofa": 3,
          "Begin": 1956,
          "End": 1964,
          "RefOntologyConceptArr": 12774,
          "TypeID": 2,
          "UmlsConcepts": [
            {
              "Id": 12764,
              "Code": 51742006,
              "CodingScheme": "SNOMEDCT_US",
              "Score": 0,
              "CUI": "C0751437",
              "TUI": "T047",
              "PreferredText": "Adenohypophyseal Diseases"
            }
          ]
        }
      ],
      [
        {
          "Id": 12865,
          "MentionName": "DiseaseDisorderMention",
          "RefSofa": 3,
          "Begin": 3639,
          "End": 3647,
          "RefOntologyConceptArr": 12862,
          "TypeID": 2,
          "UmlsConcepts": [
            {
              "Id": 12852,
              "Code": 51742006,
              "CodingScheme": "SNOMEDCT_US",
              "Score": 0,
              "CUI": "C0751437",
              "TUI": "T047",
              "PreferredText": "Adenohypophyseal Diseases"
            }
          ]
        }
      ],
      [
        {
          "Id": 12733,
          "MentionName": "DiseaseDisorderMention",
          "RefSofa": 3,
          "Begin": 3685,
          "End": 3693,
          "RefOntologyConceptArr": 12730,
          "TypeID": 2,
          "UmlsConcepts": [
            {
              "Id": 12720,
              "Code": 51742006,
              "CodingScheme": "SNOMEDCT_US",
              "Score": 0,
              "CUI": "C0751437",
              "TUI": "T047",
              "PreferredText": "Adenohypophyseal Diseases"
            }
          ]
        }
      ]
    ]
  }
  ]
