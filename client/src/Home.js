import React from "react"
import { Container, Row, Col } from "reactstrap"
import python from "./images/python_1k.png"
import pythonC from "./images/python_c_1k.png"
import csharp from "./images/csharp_1k.png"
import cplusplus from "./images/cplusplus_1k.png"

function Home() {
  return (
    <Container>
      <h2>Abstract</h2>
      <p>
        The concept of approximate string matching or “fuzzy” matching is
        important in any industry that makes use of data mining or aggregation.
        Some example industries include data driven marking, genetic sequencing,
        plagiarism detection and spam detection, just to name a few. There are
        many different algorithms used to fuzzy match data sets. For this
        project we chose to look at the Levenshtein Distance algorithm
        implemented in three languages: Python, C++ and C#. By running
        benchmarks on static datasets using implementations in the different
        languages, we found that a C-based Python approach to the Levenshtein
        Distance algorithm was the most efficient. We then designed a web
        application that allows a user to upload data sets and perform a "fuzzy"
        match to identify data records that may be equal between two of the data
        sets.
      </p>
      <h2>Introduction</h2>
      <p>
        Approximate string matching, also known as “fuzzy” matching, is a
        methodology for identifying strings of data that are approximately
        equal, but not exactly equal. There are many different applications for
        approximate string matching within computer science.
      </p>
      <p>
        Applications for approximate string matching include scenarios such as
        identifying duplicate records within a dataset and linking two records
        from disparate data sets that may not have equivalent properties.
      </p>
      <h2>Research Question(s)</h2>
      <p>
        Is there a certain programming language, between C++, Python, and C#
        that has a more efficient output of the Levenshtein Distance?
      </p>
      <p>
        What would an end user application look like that would enable
        performing a match on two datasets and reviewing the results?
      </p>
      <h2>Materials and Methods</h2>
      <p>
        In Phase 1 of our project we examined the Levenshtein Distance algorithm
        in three programming languages: Python, C++, and C#. For the first phase
        we chose to look at implementations where one string was matched against
        a data set. In examining each implementation we benchmarked the programs
        by keeping the size of the dataset static and increasing the length of
        data records that were under test. By changing the data in this way we
        could better test the algorithm efficiency in each language since more
        data would be fed into the matching algorithm.
      </p>
      <p>
        In Phase 2, we first attempted to create implementations of the
        Levenshtein Distance algorithm with multithreading in the three
        programming languages from Phase 1. This proved to be difficult to do in
        Python and C++. While we did get it to work we decided to use the
        benchmarks from Phase 1 to determine how to proceed with the user based
        application.
      </p>
      <p>
        For the final phase of our project we designed and created a user
        friendly web application to easily compare data sets. We focused on ease
        of use and the applications ability to compare and return the identified
        matches. Since this project was focused on the implementation of
        performing a comparison or match of two datasets, typical web
        application features like user authentication were omitted. The web
        application was developed using modern techniques and languages. The
        application’s server side code is written in Microsoft’s latest version
        of .Net Core running on a linux docker container which serves a ReactJS
        client. The code is built and published to docker hub using the Open
        Source CI/CD tool TravisCI.
      </p>
      <h2>Benchmarking Results</h2>
      <Row>
        <Col lg="6">
          <img src={python} alt="Python Benchmarks" width="100%" />
        </Col>
        <Col lg="6">
          <img src={csharp} alt="C# Benchmarks" width="100%" />
        </Col>
      </Row>
      <Row>
        <Col lg="6">
          <img src={pythonC} alt="Python w/C Package Benchmarks" width="100%" />
        </Col>
        <Col lg="6">
          <img src={cplusplus} alt="C++ Benchmarks" width="100%" />
        </Col>
      </Row>
      <h2>Conclusion</h2>
      <p>
        When comparing the results of the benchmarking for the Python, C# and
        C++ implementations of the Levenshtein Distance algorithm, we can
        clearly see that C# and C++ implementations perform more than 100x
        faster than the python implementation. We initially thought python would
        be slow due to the interpreted nature of the language, and we can see
        here that that is indeed the case. What is more interesting, however,
        are the results of using a C based python package; using the same
        generated dataset, we were unable to get accurate benchmarks due to how
        fast the compiled C package performed. In order to get readings similar
        to the other implementations we had to increase the dataset size
        exponentially.
      </p>
      <p>
        The web application portion of this project was successful. The
        application allows the user to upload and store multiple datasets and
        then perform a Levenshtein based fuzzy match comparison between any two
        of the previously stored datasets. Doing this results in a third dataset
        that contains row numbers and similarity scores for potentially
        equivalent records based on a threshold that the user sets.
      </p>
      <h2>Future Work</h2>
      <p>
        The web application for this project has limited functionality. For this
        project we focused on uploading data, performing the fuzzy match, and
        displaying the results. Future iterations of the application could
        integrate user based security for accessing and storing data as well as
        the ability to choose an algorithm implementation to perform the match.
        This would allow the application to be useful for a more varieties of
        datasets. For example, if the datasets contained DNA sequences then the
        Smith-Waterman algorithm would more suited to identify the similarities
        in the data.
      </p>
      <p>
        While initially intended, this project scope was unable to include
        performing benchmarks of the Levenshtein algorithm against a GPU based
        implementation. Our hypothesis is that since the algorithms are
        predominately CPU bound work, a GPU would be better suited to process
        high volumes of data. Previous research in this area focused on exact
        string matching algorithms and found the GPU implementations to be 24x
        faster than their CPU based counterparts. (S. Kouzinopoulous et. all,
        2009)
      </p>
      <h2>Contact Information</h2>
      <p>
        Website:{" "}
        <a href="http://fuzzy.eastus.cloudapp.azure.com/" target="_new">
          http://fuzzy.eastus.cloudapp.azure.com/
        </a>
        <br />
        App Source:{" "}
        <a href="https://github.com/lilasquared/FuzzyMatching" target="_new">
          https://github.com/lilasquared/FuzzyMatching
        </a>
        <br />
        Benchmark Source:{" "}
        <a href="https://github.com/PaleDan/CS4850_FuzzyMatching" target="_new">
          https://github.com/PaleDan/CS4850_FuzzyMatching
        </a>
        <br />
      </p>
      <p>
        Contacts: <br />
        Aaron Roberts: e:{" "}
        <a href="mailto:aaronmroberts1@gmail.com">aaronmroberts1@gmail.com</a>
        p: 404-368-2881 <br />
        Daniel Childers: e:{" "}
        <a href="mailto:danielchilders7@gmail.com">danielchilders7@gmail.com</a>
        p: 678-270-0913 <br />
        Jonathan Miu: e:{" "}
        <a href="mailto:miu.jonathan@gmail.com">miu.jonathan@gmail.com</a>
        p: 678-665-7120 <br />
        Lea Nooyen: e:{" "}
        <a href="mailto:lnooyen40@gmail.com">lnooyen40@gmail.com</a>
        p: 608-320-4399 <br />
        Chase Woods: e:{" "}
        <a href="mailto:woodj938@gmail.com">woodj938@gmail.com</a> p:
        470-309-5956
      </p>
      <h2>References</h2>
      <p>
        Rosetta Code{" "}
        <a
          href="http://www.rosettacode.org/wiki/Levenshtein_distance"
          target="_new"
        >
          http://www.rosettacode.org/wiki/Levenshtein_distance
        </a>
      </p>
      <p>
        S. Kouzinopoulos, Charalampos &amp; Margaritis, Konstantinos G. (2009).
        String Matching on a multicore GPU using CUDA. 14-18.
        10.1109/PCI.2009.47
      </p>
    </Container>
  )
}

export default Home
