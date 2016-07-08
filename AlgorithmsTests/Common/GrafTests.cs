using Algorithms.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Algorithms.AlgorithmsTests.Common
{
	public class GrafTests
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public GrafTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public void DFSTest1()
		{
			var graf = new Graf<char>();

			var qVertex = new Vertex<char>('q');
			var rVertex = new Vertex<char>('r');
			var sVertex = new Vertex<char>('s');
			var tVertex = new Vertex<char>('t');
			var uVertex = new Vertex<char>('u');
			var vVertex = new Vertex<char>('v');
			var wVertex = new Vertex<char>('w');
			var xVertex = new Vertex<char>('x');
			var yVertex = new Vertex<char>('y');
			var zVertex = new Vertex<char>('z');

			qVertex.AddDirectedEdge(1, sVertex);
			qVertex.AddDirectedEdge(1, tVertex);
			qVertex.AddDirectedEdge(1, wVertex);

			sVertex.AddDirectedEdge(1, vVertex);

			vVertex.AddDirectedEdge(1, wVertex);

			wVertex.AddDirectedEdge(1, sVertex);
			
			tVertex.AddDirectedEdge(1, xVertex);
			tVertex.AddDirectedEdge(1, yVertex);

			xVertex.AddDirectedEdge(1, zVertex);
			
			yVertex.AddDirectedEdge(1, qVertex);

			rVertex.AddDirectedEdge(1, uVertex);
			rVertex.AddDirectedEdge(1, yVertex);
			
			uVertex.AddDirectedEdge(1, yVertex);

			graf.AddVertex(qVertex);
			graf.AddVertex(rVertex);
			graf.AddVertex(sVertex);
			graf.AddVertex(tVertex);
			graf.AddVertex(uVertex);
			graf.AddVertex(vVertex);
			graf.AddVertex(wVertex);
			graf.AddVertex(xVertex);
			graf.AddVertex(yVertex);
			graf.AddVertex(zVertex);

			graf.DFS();

			Assert.Equal(1, qVertex.DiscoveryTime);
			Assert.Equal(16, qVertex.FinishingTime);

			Assert.Equal(17, rVertex.DiscoveryTime);
			Assert.Equal(20, rVertex.FinishingTime);

			Assert.Equal(2, sVertex.DiscoveryTime);
			Assert.Equal(7, sVertex.FinishingTime);

			Assert.Equal(8, tVertex.DiscoveryTime);
			Assert.Equal(15, tVertex.FinishingTime);

			Assert.Equal(18, uVertex.DiscoveryTime);
			Assert.Equal(19, uVertex.FinishingTime);

			Assert.Equal(3, vVertex.DiscoveryTime);
			Assert.Equal(6, vVertex.FinishingTime);

			Assert.Equal(4, wVertex.DiscoveryTime);
			Assert.Equal(5, wVertex.FinishingTime);

			Assert.Equal(9, xVertex.DiscoveryTime);
			Assert.Equal(12, xVertex.FinishingTime);

			Assert.Equal(13, yVertex.DiscoveryTime);
			Assert.Equal(14, yVertex.FinishingTime);

			Assert.Equal(10, zVertex.DiscoveryTime);
			Assert.Equal(11, zVertex.FinishingTime);
		}

		[Fact]
		public void TopologicalTest1()
		{
			var graf = new Graf<char>();

			var qVertex = new Vertex<char>('q');
			var rVertex = new Vertex<char>('r');
			var sVertex = new Vertex<char>('s');
			var tVertex = new Vertex<char>('t');
			var uVertex = new Vertex<char>('u');
			var vVertex = new Vertex<char>('v');
			var wVertex = new Vertex<char>('w');
			var xVertex = new Vertex<char>('x');
			var yVertex = new Vertex<char>('y');
			var zVertex = new Vertex<char>('z');

			qVertex.AddDirectedEdge(1, sVertex);
			qVertex.AddDirectedEdge(1, tVertex);
			qVertex.AddDirectedEdge(1, wVertex);

			sVertex.AddDirectedEdge(1, vVertex);

			vVertex.AddDirectedEdge(1, wVertex);

			wVertex.AddDirectedEdge(1, sVertex);

			tVertex.AddDirectedEdge(1, xVertex);
			tVertex.AddDirectedEdge(1, yVertex);

			xVertex.AddDirectedEdge(1, zVertex);

			yVertex.AddDirectedEdge(1, qVertex);

			rVertex.AddDirectedEdge(1, uVertex);
			rVertex.AddDirectedEdge(1, yVertex);

			uVertex.AddDirectedEdge(1, yVertex);

			graf.AddVertex(qVertex);
			graf.AddVertex(rVertex);
			graf.AddVertex(sVertex);
			graf.AddVertex(tVertex);
			graf.AddVertex(uVertex);
			graf.AddVertex(vVertex);
			graf.AddVertex(wVertex);
			graf.AddVertex(xVertex);
			graf.AddVertex(yVertex);
			graf.AddVertex(zVertex);

			graf.DFS();

			Assert.Null(graf.GetTopologicalSort());
		}

		[Fact]
		public void TopologicalTest2()
		{
			var graf = new Graf<char>();

			var mVertex = new Vertex<char>('m');
			var nVertex = new Vertex<char>('n');
			var oVertex = new Vertex<char>('o');
			var pVertex = new Vertex<char>('p');

			var qVertex = new Vertex<char>('q');
			var rVertex = new Vertex<char>('r');
			var sVertex = new Vertex<char>('s');

			var tVertex = new Vertex<char>('t');
			var uVertex = new Vertex<char>('u');
			var vVertex = new Vertex<char>('v');
			var wVertex = new Vertex<char>('w');

			var xVertex = new Vertex<char>('x');
			var yVertex = new Vertex<char>('y');
			var zVertex = new Vertex<char>('z');

			mVertex.AddDirectedEdge(1, qVertex);
			mVertex.AddDirectedEdge(1, rVertex);
			mVertex.AddDirectedEdge(1, xVertex);

			nVertex.AddDirectedEdge(1, qVertex);
			nVertex.AddDirectedEdge(1, uVertex);
			nVertex.AddDirectedEdge(1, oVertex);

			oVertex.AddDirectedEdge(1, rVertex);
			oVertex.AddDirectedEdge(1, sVertex);

			pVertex.AddDirectedEdge(1, oVertex);
			pVertex.AddDirectedEdge(1, sVertex);
			pVertex.AddDirectedEdge(1, zVertex);

			qVertex.AddDirectedEdge(1, tVertex);

			rVertex.AddDirectedEdge(1, uVertex);
			rVertex.AddDirectedEdge(1, yVertex);

			sVertex.AddDirectedEdge(1, rVertex);

			uVertex.AddDirectedEdge(1, tVertex);

			vVertex.AddDirectedEdge(1, wVertex);
			vVertex.AddDirectedEdge(1, xVertex);

			wVertex.AddDirectedEdge(1, zVertex);

			yVertex.AddDirectedEdge(1, vVertex);

			graf.AddVertex(mVertex);
			graf.AddVertex(nVertex);
			graf.AddVertex(oVertex);
			graf.AddVertex(pVertex);
			graf.AddVertex(qVertex);
			graf.AddVertex(rVertex);
			graf.AddVertex(sVertex);
			graf.AddVertex(tVertex);
			graf.AddVertex(uVertex);
			graf.AddVertex(vVertex);
			graf.AddVertex(wVertex);
			graf.AddVertex(xVertex);
			graf.AddVertex(yVertex);
			graf.AddVertex(zVertex);

			graf.DFS();

			var topologicalSort = graf.GetTopologicalSort().ToList();
			Assert.NotNull(topologicalSort);
			Assert.Equal(14, topologicalSort.Count);
			Assert.Equal('p', topologicalSort.First().Key);
			Assert.Equal(27, topologicalSort.First().DiscoveryTime);
			Assert.Equal(28, topologicalSort.First().FinishingTime);

			Assert.Equal('t', topologicalSort.Last().Key);
			Assert.Equal(3, topologicalSort.Last().DiscoveryTime);
			Assert.Equal(4, topologicalSort.Last().FinishingTime);
		}

		[Fact]
		public void TopologicalTest3()
		{
			var graf = new Graf<char>();

			var mVertex = new Vertex<char>('m');
			var nVertex = new Vertex<char>('n');
			var oVertex = new Vertex<char>('o');
			var pVertex = new Vertex<char>('p');

			var qVertex = new Vertex<char>('q');
			var rVertex = new Vertex<char>('r');
			var sVertex = new Vertex<char>('s');

			var tVertex = new Vertex<char>('t');
			var uVertex = new Vertex<char>('u');
			var vVertex = new Vertex<char>('v');
			var wVertex = new Vertex<char>('w');

			var xVertex = new Vertex<char>('x');
			var yVertex = new Vertex<char>('y');
			var zVertex = new Vertex<char>('z');

			mVertex.AddDirectedEdge(1, qVertex);
			mVertex.AddDirectedEdge(1, rVertex);
			mVertex.AddDirectedEdge(1, xVertex);

			nVertex.AddDirectedEdge(1, qVertex);
			nVertex.AddDirectedEdge(1, uVertex);
			nVertex.AddDirectedEdge(1, oVertex);

			oVertex.AddDirectedEdge(1, rVertex);
			oVertex.AddDirectedEdge(1, sVertex);

			pVertex.AddDirectedEdge(1, oVertex);
			pVertex.AddDirectedEdge(1, sVertex);
			pVertex.AddDirectedEdge(1, zVertex);

			qVertex.AddDirectedEdge(1, tVertex);

			rVertex.AddDirectedEdge(1, uVertex);
			rVertex.AddDirectedEdge(1, yVertex);

			sVertex.AddDirectedEdge(1, rVertex);

			uVertex.AddDirectedEdge(1, tVertex);

			vVertex.AddDirectedEdge(1, wVertex);
			vVertex.AddDirectedEdge(1, xVertex);

			wVertex.AddDirectedEdge(1, zVertex);

			yVertex.AddDirectedEdge(1, vVertex);

			graf.AddVertex(mVertex);
			graf.AddVertex(nVertex);
			graf.AddVertex(oVertex);
			graf.AddVertex(pVertex);
			graf.AddVertex(qVertex);
			graf.AddVertex(rVertex);
			graf.AddVertex(sVertex);
			graf.AddVertex(tVertex);
			graf.AddVertex(uVertex);
			graf.AddVertex(vVertex);
			graf.AddVertex(wVertex);
			graf.AddVertex(xVertex);
			graf.AddVertex(yVertex);
			graf.AddVertex(zVertex);

			graf.DFS();

			var topologicalSort = graf.GetTopologicalSort().ToList();
			Assert.NotNull(topologicalSort);
			Assert.Equal(14, topologicalSort.Count);
			Assert.Equal('p', topologicalSort.First().Key);
			Assert.Equal(27, topologicalSort.First().DiscoveryTime);
			Assert.Equal(28, topologicalSort.First().FinishingTime);

			Assert.Equal('t', topologicalSort.Last().Key);
			Assert.Equal(3, topologicalSort.Last().DiscoveryTime);
			Assert.Equal(4, topologicalSort.Last().FinishingTime);

			var grafT = graf.GetGrafT();
			var components = grafT.GetStronglyConnectedComponents(topologicalSort);
			Assert.Equal(14, components.Count);
		}

		[Fact]
		public void TopologicalTest4()
		{
			var graf = new Graf<char>();

			var bVertex = new Vertex<char>('b');
			var cVertex = new Vertex<char>('c');
			var dVertex = new Vertex<char>('d');

			var aVertex = new Vertex<char>('a');
			var iVertex = new Vertex<char>('i');
			var eVertex = new Vertex<char>('e');

			var hVertex = new Vertex<char>('h');
			var gVertex = new Vertex<char>('g');
			var fVertex = new Vertex<char>('f');

			aVertex.AddUnDirectedEdge(4, bVertex);
			bVertex.AddUnDirectedEdge(8, cVertex);
			cVertex.AddUnDirectedEdge(7, dVertex);

			dVertex.AddUnDirectedEdge(9, eVertex);
			dVertex.AddUnDirectedEdge(14, fVertex);
			eVertex.AddUnDirectedEdge(10, fVertex);

			aVertex.AddUnDirectedEdge(8, hVertex);
			bVertex.AddUnDirectedEdge(11, hVertex);

			cVertex.AddUnDirectedEdge(2, iVertex);
			cVertex.AddUnDirectedEdge(4, fVertex);
			iVertex.AddUnDirectedEdge(7, hVertex);

			iVertex.AddUnDirectedEdge(6, gVertex);

			hVertex.AddUnDirectedEdge(1, gVertex);
			gVertex.AddUnDirectedEdge(2, fVertex);

			graf.AddVertex(aVertex);
			graf.AddVertex(bVertex);
			graf.AddVertex(cVertex);
			graf.AddVertex(dVertex);
			graf.AddVertex(iVertex);
			graf.AddVertex(eVertex);
			graf.AddVertex(hVertex);
			graf.AddVertex(gVertex);
			graf.AddVertex(fVertex);

			var components = graf.GetStronglyConnectedComponentsWithUnions();

			Assert.Equal(1, components.Count);
		}

		[Fact]
		public void MSTTest0()
		{
			var graf = new Graf<char>();

			var bVertex = new Vertex<char>('b');
			var cVertex = new Vertex<char>('c');
			var dVertex = new Vertex<char>('d');

			var aVertex = new Vertex<char>('a');
			var iVertex = new Vertex<char>('i');
			var eVertex = new Vertex<char>('e');

			var hVertex = new Vertex<char>('h');
			var gVertex = new Vertex<char>('g');
			var fVertex = new Vertex<char>('f');

			aVertex.AddUnDirectedEdge(4, bVertex);
			bVertex.AddUnDirectedEdge(8, cVertex);
			cVertex.AddUnDirectedEdge(7, dVertex);

			dVertex.AddUnDirectedEdge(9, eVertex);
			dVertex.AddUnDirectedEdge(14, fVertex);
			eVertex.AddUnDirectedEdge(10, fVertex);

			aVertex.AddUnDirectedEdge(8, hVertex);
			bVertex.AddUnDirectedEdge(11, hVertex);

			cVertex.AddUnDirectedEdge(2, iVertex);
			cVertex.AddUnDirectedEdge(4, fVertex);
			iVertex.AddUnDirectedEdge(7, hVertex);

			iVertex.AddUnDirectedEdge(6, gVertex);

			hVertex.AddUnDirectedEdge(1, gVertex);
			gVertex.AddUnDirectedEdge(2, fVertex);

			graf.AddVertex(aVertex);
			graf.AddVertex(bVertex);
			graf.AddVertex(cVertex);
			graf.AddVertex(dVertex);
			graf.AddVertex(iVertex);
			graf.AddVertex(eVertex);
			graf.AddVertex(hVertex);
			graf.AddVertex(gVertex);
			graf.AddVertex(fVertex);

			var mstGraf = graf.GetMSTGrafPrim(aVertex);

			double weigth = 0;
			for (int i = 0; i < mstGraf.Vertexes.Count; i++)
			{
				for (int j = 0; j < mstGraf.Vertexes[i].Edges.Count; j++)
				{
					mstGraf.Vertexes[i].Edges[j].Marked = false;
				}
			}
			for (int i = 0; i < mstGraf.Vertexes.Count; i++)
			{
				for (int j = 0; j < mstGraf.Vertexes[i].Edges.Count; j++)
				{
					var adjacentVertex = mstGraf.Vertexes[i].Edges[j].Other(mstGraf.Vertexes[i]);
					if (!mstGraf.Vertexes[i].Edges[j].Marked)
					{
						weigth += mstGraf.Vertexes[i].Edges[j].Weigth;
						mstGraf.Vertexes[i].Edges[j].Marked = true;
					}
				}
			}
			Assert.Equal(37, weigth);
		}

		[Fact]
		public void MSTTest1()
		{
			var graf = new Graf<char>();

			var bVertex = new Vertex<char>('b');
			var cVertex = new Vertex<char>('c');
			var dVertex = new Vertex<char>('d');

			var aVertex = new Vertex<char>('a');
			var iVertex = new Vertex<char>('i');
			var eVertex = new Vertex<char>('e');

			var hVertex = new Vertex<char>('h');
			var gVertex = new Vertex<char>('g');
			var fVertex = new Vertex<char>('f');

			aVertex.AddUnDirectedEdge(4, bVertex);
			bVertex.AddUnDirectedEdge(8, cVertex);
			cVertex.AddUnDirectedEdge(7, dVertex);

			dVertex.AddUnDirectedEdge(9, eVertex);
			dVertex.AddUnDirectedEdge(14, fVertex);
			eVertex.AddUnDirectedEdge(10, fVertex);

			aVertex.AddUnDirectedEdge(8, hVertex);
			bVertex.AddUnDirectedEdge(11, hVertex);

			cVertex.AddUnDirectedEdge(2, iVertex);
			cVertex.AddUnDirectedEdge(4, fVertex);
			iVertex.AddUnDirectedEdge(7, hVertex);

			iVertex.AddUnDirectedEdge(6, gVertex);

			hVertex.AddUnDirectedEdge(1, gVertex);
			gVertex.AddUnDirectedEdge(2, fVertex);

			graf.AddVertex(aVertex);
			graf.AddVertex(bVertex);
			graf.AddVertex(cVertex);
			graf.AddVertex(dVertex);
			graf.AddVertex(iVertex);
			graf.AddVertex(eVertex);
			graf.AddVertex(hVertex);
			graf.AddVertex(gVertex);
			graf.AddVertex(fVertex);

			var mstGraf = graf.GetMSTGrafKruskal();

			double weigth = 0;
			for (int i = 0; i < mstGraf.Vertexes.Count; i++)
			{
				for (int j = 0; j < mstGraf.Vertexes[i].Edges.Count; j++)
				{
					mstGraf.Vertexes[i].Edges[j].Marked = false;
				}
			}
			for (int i = 0; i < mstGraf.Vertexes.Count; i++)
			{
				for (int j = 0; j < mstGraf.Vertexes[i].Edges.Count; j++)
				{
					var adjacentVertex = mstGraf.Vertexes[i].Edges[j].Other(mstGraf.Vertexes[i]);
					if (!mstGraf.Vertexes[i].Edges[j].Marked)
					{
						weigth += mstGraf.Vertexes[i].Edges[j].Weigth;
						mstGraf.Vertexes[i].Edges[j].Marked = true;
					}
				}
			}
			Assert.Equal(37, weigth);
		}

		[Fact]
		public void SingleSourceTest0()
		{
			var graf = new Graf<char>();

			var sVertex = new Vertex<char>('s');
			var tVertex = new Vertex<char>('t');
			var xVertex = new Vertex<char>('x');
			var yVertex = new Vertex<char>('y');

			var zVertex = new Vertex<char>('z');

			sVertex.AddDirectedEdge(6, tVertex);
			sVertex.AddDirectedEdge(7, yVertex);
			tVertex.AddDirectedEdge(5, xVertex);

			xVertex.AddDirectedEdge(-2, tVertex);
			tVertex.AddDirectedEdge(8, yVertex);
			zVertex.AddDirectedEdge(7, xVertex);

			zVertex.AddDirectedEdge(2, sVertex);
			tVertex.AddDirectedEdge(-4, zVertex);

			yVertex.AddDirectedEdge(-3, xVertex);
			yVertex.AddDirectedEdge(9, zVertex);

			graf.AddVertex(sVertex);
			graf.AddVertex(tVertex);
			graf.AddVertex(xVertex);
			graf.AddVertex(yVertex);
			graf.AddVertex(zVertex);

			bool res = graf.BellmanFordShortestPaths(sVertex);

			Assert.Equal(true, res);
			Assert.Equal(0, sVertex.DiscoveryTime);
			Assert.Equal(2, tVertex.DiscoveryTime);
			Assert.Equal(4, xVertex.DiscoveryTime);
			Assert.Equal(7, yVertex.DiscoveryTime);
			Assert.Equal(-2, zVertex.DiscoveryTime);
		}

		[Fact]
		public void SingleSourceTest1()
		{
			var graf = new Graf<char>();

			var sVertex = new Vertex<char>('s');
			var tVertex = new Vertex<char>('t');
			var xVertex = new Vertex<char>('x');
			var yVertex = new Vertex<char>('y');

			var zVertex = new Vertex<char>('z');

			sVertex.AddDirectedEdge(6, tVertex);
			sVertex.AddDirectedEdge(7, yVertex);
			tVertex.AddDirectedEdge(-5, xVertex);

			xVertex.AddDirectedEdge(2, tVertex);
			tVertex.AddDirectedEdge(8, yVertex);
			zVertex.AddDirectedEdge(7, xVertex);

			zVertex.AddDirectedEdge(2, sVertex);
			tVertex.AddDirectedEdge(-4, zVertex);

			yVertex.AddDirectedEdge(-3, xVertex);
			yVertex.AddDirectedEdge(9, zVertex);

			graf.AddVertex(sVertex);
			graf.AddVertex(tVertex);
			graf.AddVertex(xVertex);
			graf.AddVertex(yVertex);
			graf.AddVertex(zVertex);

			bool res = graf.BellmanFordShortestPaths(sVertex);

			Assert.Equal(false, res);
		}

		[Fact]
		public void SingleSourceTest2()
		{
			var graf = new Graf<char>();

			var rVertex = new Vertex<char>('r');
			var sVertex = new Vertex<char>('s');
			var tVertex = new Vertex<char>('t');
			var xVertex = new Vertex<char>('x');
			var yVertex = new Vertex<char>('y');

			var zVertex = new Vertex<char>('z');

			rVertex.AddDirectedEdge(5, sVertex);
			rVertex.AddDirectedEdge(3, tVertex);
			sVertex.AddDirectedEdge(2, tVertex);
			sVertex.AddDirectedEdge(6, xVertex);
			tVertex.AddDirectedEdge(7, xVertex);

			tVertex.AddDirectedEdge(4, yVertex);
			tVertex.AddDirectedEdge(2, zVertex);
			xVertex.AddDirectedEdge(-1, yVertex);

			xVertex.AddDirectedEdge(-2, zVertex);
			yVertex.AddDirectedEdge(-2, zVertex);

			graf.AddVertex(rVertex);
			graf.AddVertex(sVertex);
			graf.AddVertex(tVertex);
			graf.AddVertex(xVertex);
			graf.AddVertex(yVertex);
			graf.AddVertex(zVertex);

			graf.TopologicalShortestPaths(sVertex);
			var topologicalSort = graf.GetTopologicalSort().ToList();

			Assert.NotNull(topologicalSort);
			Assert.Equal(double.PositiveInfinity, rVertex.DiscoveryTime);
			Assert.Equal(0, sVertex.DiscoveryTime);
			Assert.Equal(2, tVertex.DiscoveryTime);
			Assert.Equal(6, xVertex.DiscoveryTime);
			Assert.Equal(5, yVertex.DiscoveryTime);
			Assert.Equal(3, zVertex.DiscoveryTime);
		}

		[Fact]
		public void SingleSourceTest3()
		{
			var graf = new Graf<char>();

			var sVertex = new Vertex<char>('s');
			var tVertex = new Vertex<char>('t');
			var xVertex = new Vertex<char>('x');
			var yVertex = new Vertex<char>('y');

			var zVertex = new Vertex<char>('z');

			sVertex.AddDirectedEdge(10, tVertex);
			sVertex.AddDirectedEdge(5, yVertex);
			tVertex.AddDirectedEdge(1, xVertex);

			tVertex.AddDirectedEdge(2, yVertex);
			xVertex.AddDirectedEdge(4, zVertex);
			zVertex.AddDirectedEdge(6, xVertex);

			zVertex.AddDirectedEdge(7, sVertex);
			yVertex.AddDirectedEdge(3, tVertex);

			yVertex.AddDirectedEdge(9, xVertex);
			yVertex.AddDirectedEdge(2, zVertex);

			graf.AddVertex(sVertex);
			graf.AddVertex(tVertex);
			graf.AddVertex(xVertex);
			graf.AddVertex(yVertex);
			graf.AddVertex(zVertex);

			graf.DejkstraShortestPaths(sVertex);

			Assert.Equal(0, sVertex.DiscoveryTime);
			Assert.Equal(8, tVertex.DiscoveryTime);
			Assert.Equal(9, xVertex.DiscoveryTime);
			Assert.Equal(5, yVertex.DiscoveryTime);
			Assert.Equal(7, zVertex.DiscoveryTime);
		}

		[Fact]
		public void DifferenceTest0()
		{
			var graf = new Graf<char>();

			var v0Vertex = new Vertex<char>('0');
			var v1Vertex = new Vertex<char>('1');
			var v2Vertex = new Vertex<char>('2');
			var v3Vertex = new Vertex<char>('3');

			var v4Vertex = new Vertex<char>('4');
			var v5Vertex = new Vertex<char>('5');

			v0Vertex.AddDirectedEdge(0, v1Vertex);
			v0Vertex.AddDirectedEdge(0, v2Vertex);
			v0Vertex.AddDirectedEdge(0, v3Vertex);
			v0Vertex.AddDirectedEdge(0, v4Vertex);
			v0Vertex.AddDirectedEdge(0, v5Vertex);

			v1Vertex.AddDirectedEdge(5, v3Vertex);
			v1Vertex.AddDirectedEdge(4, v4Vertex);

			v2Vertex.AddDirectedEdge(0, v1Vertex);

			v3Vertex.AddDirectedEdge(-1, v4Vertex);
			v3Vertex.AddDirectedEdge(-3, v5Vertex);

			v4Vertex.AddDirectedEdge(-3, v5Vertex);

			v5Vertex.AddDirectedEdge(-1, v1Vertex);
			v5Vertex.AddDirectedEdge(1, v2Vertex);

			graf.AddVertex(v0Vertex);
			graf.AddVertex(v1Vertex);
			graf.AddVertex(v2Vertex);
			graf.AddVertex(v3Vertex);
			graf.AddVertex(v4Vertex);
			graf.AddVertex(v5Vertex);

			bool res = graf.BellmanFordShortestPaths(v0Vertex);

			Assert.Equal(true, res);
			Assert.Equal(0, v0Vertex.DiscoveryTime);
			Assert.Equal(-5, v1Vertex.DiscoveryTime);
			Assert.Equal(-3, v2Vertex.DiscoveryTime);
			Assert.Equal(0, v3Vertex.DiscoveryTime);
			Assert.Equal(-1, v4Vertex.DiscoveryTime);
			Assert.Equal(-4, v5Vertex.DiscoveryTime);
		}

		[Fact]
		public void ShortestsPathsTest0()
		{
			var graf = new Graf<char>();

			var v1Vertex = new Vertex<char>('1');
			var v2Vertex = new Vertex<char>('2');
			var v3Vertex = new Vertex<char>('3');
			var v4Vertex = new Vertex<char>('4');
			var v5Vertex = new Vertex<char>('5');

			v1Vertex.AddDirectedEdge(3, v2Vertex);
			v1Vertex.AddDirectedEdge(-4, v5Vertex);

			v2Vertex.AddDirectedEdge(1, v4Vertex);
			v2Vertex.AddDirectedEdge(7, v5Vertex);

			v3Vertex.AddDirectedEdge(4, v2Vertex);

			v4Vertex.AddDirectedEdge(2, v1Vertex);
			v4Vertex.AddDirectedEdge(-5, v3Vertex);

			v5Vertex.AddDirectedEdge(6, v4Vertex);

			graf.AddVertex(v1Vertex);
			graf.AddVertex(v2Vertex);
			graf.AddVertex(v3Vertex);
			graf.AddVertex(v4Vertex);
			graf.AddVertex(v5Vertex);

			var res = graf.GetAllPairsShortestsPaths(graf.GetWeights());

			Assert.Equal(0, res[0, 0]);
			Assert.Equal(3, res[1, 0]);
			Assert.Equal(7, res[2, 0]);
			Assert.Equal(2, res[3, 0]);
			Assert.Equal(8, res[4, 0]);

			Assert.Equal(1, res[0, 1]);
			Assert.Equal(0, res[1, 1]);
			Assert.Equal(4, res[2, 1]);
			Assert.Equal(-1, res[3, 1]);
			Assert.Equal(5, res[4, 1]);

			Assert.Equal(-3, res[0, 2]);
			Assert.Equal(-4, res[1, 2]);
			Assert.Equal(0, res[2, 2]);
			Assert.Equal(-5, res[3, 2]);
			Assert.Equal(1, res[4, 2]);

			Assert.Equal(2, res[0, 3]);
			Assert.Equal(1, res[1, 3]);
			Assert.Equal(5, res[2, 3]);
			Assert.Equal(0, res[3, 3]);
			Assert.Equal(6, res[4, 3]);

			Assert.Equal(-4, res[0, 4]);
			Assert.Equal(-1, res[1, 4]);
			Assert.Equal(3, res[2, 4]);
			Assert.Equal(-2, res[3, 4]);
			Assert.Equal(0, res[4, 4]);
		}

		[Fact]
		public void ShortestsPathsTest1()
		{
			var graf = new Graf<char>();

			var v1Vertex = new Vertex<char>('1');
			var v2Vertex = new Vertex<char>('2');
			var v3Vertex = new Vertex<char>('3');
			var v4Vertex = new Vertex<char>('4');
			var v5Vertex = new Vertex<char>('5');

			v1Vertex.AddDirectedEdge(3, v2Vertex);
			v1Vertex.AddDirectedEdge(-4, v5Vertex);

			v2Vertex.AddDirectedEdge(1, v4Vertex);
			v2Vertex.AddDirectedEdge(7, v5Vertex);

			v3Vertex.AddDirectedEdge(4, v2Vertex);

			v4Vertex.AddDirectedEdge(2, v1Vertex);
			v4Vertex.AddDirectedEdge(-5, v3Vertex);

			v5Vertex.AddDirectedEdge(6, v4Vertex);

			graf.AddVertex(v1Vertex);
			graf.AddVertex(v2Vertex);
			graf.AddVertex(v3Vertex);
			graf.AddVertex(v4Vertex);
			graf.AddVertex(v5Vertex);

			var res = graf.GetAllPairsShortestsPathsFloydWarshall(graf.GetWeights());

			Assert.Equal(0, res.Item1[0, 0]);
			Assert.Equal(3, res.Item1[1, 0]);
			Assert.Equal(7, res.Item1[2, 0]);
			Assert.Equal(2, res.Item1[3, 0]);
			Assert.Equal(8, res.Item1[4, 0]);

			Assert.Equal(1, res.Item1[0, 1]);
			Assert.Equal(0, res.Item1[1, 1]);
			Assert.Equal(4, res.Item1[2, 1]);
			Assert.Equal(-1, res.Item1[3, 1]);
			Assert.Equal(5, res.Item1[4, 1]);

			Assert.Equal(-3, res.Item1[0, 2]);
			Assert.Equal(-4, res.Item1[1, 2]);
			Assert.Equal(0, res.Item1[2, 2]);
			Assert.Equal(-5, res.Item1[3, 2]);
			Assert.Equal(1, res.Item1[4, 2]);

			Assert.Equal(2, res.Item1[0, 3]);
			Assert.Equal(1, res.Item1[1, 3]);
			Assert.Equal(5, res.Item1[2, 3]);
			Assert.Equal(0, res.Item1[3, 3]);
			Assert.Equal(6, res.Item1[4, 3]);

			Assert.Equal(-4, res.Item1[0, 4]);
			Assert.Equal(-1, res.Item1[1, 4]);
			Assert.Equal(3, res.Item1[2, 4]);
			Assert.Equal(-2, res.Item1[3, 4]);
			Assert.Equal(0, res.Item1[4, 4]);

			Assert.Null(res.Item2[0, 0]);
			Assert.Equal(3, res.Item2[1, 0].Value);
			Assert.Equal(3, res.Item2[2, 0].Value);
			Assert.Equal(3, res.Item2[3, 0].Value);
			Assert.Equal(3, res.Item2[4, 0].Value);

			Assert.Equal(2, res.Item2[0, 1].Value);
			Assert.Null(res.Item2[1, 1]);
			Assert.Equal(2, res.Item2[2, 1].Value);
			Assert.Equal(2, res.Item2[3, 1].Value);
			Assert.Equal(2, res.Item2[4, 1].Value);

			Assert.Equal(3, res.Item2[0, 2].Value);
			Assert.Equal(3, res.Item2[1, 2].Value);
			Assert.Null(res.Item2[2, 2]);
			Assert.Equal(3, res.Item2[3, 2].Value);
			Assert.Equal(3, res.Item2[4, 2].Value);

			Assert.Equal(4, res.Item2[0, 3].Value);
			Assert.Equal(1, res.Item2[1, 3].Value);
			Assert.Equal(1, res.Item2[2, 3].Value);
			Assert.Null(res.Item2[3, 3]);
			Assert.Equal(4, res.Item2[4, 3].Value);

			Assert.Equal(0, res.Item2[0, 4].Value);
			Assert.Equal(0, res.Item2[1, 4].Value);
			Assert.Equal(0, res.Item2[2, 4].Value);
			Assert.Equal(0, res.Item2[3, 4].Value);
			Assert.Null(res.Item2[4, 4]);
		}

		[Fact]
		public void ShortestsPathsTest2()
		{
			var graf = new Graf<char>();

			var v1Vertex = new Vertex<char>('1');
			var v2Vertex = new Vertex<char>('2');
			var v3Vertex = new Vertex<char>('3');
			var v4Vertex = new Vertex<char>('4');
			var v5Vertex = new Vertex<char>('5');

			v1Vertex.AddDirectedEdge(3, v2Vertex);
			v1Vertex.AddDirectedEdge(-4, v5Vertex);

			v2Vertex.AddDirectedEdge(1, v4Vertex);
			v2Vertex.AddDirectedEdge(7, v5Vertex);

			v3Vertex.AddDirectedEdge(4, v2Vertex);

			v4Vertex.AddDirectedEdge(2, v1Vertex);
			v4Vertex.AddDirectedEdge(-5, v3Vertex);

			v5Vertex.AddDirectedEdge(6, v4Vertex);

			graf.AddVertex(v1Vertex);
			graf.AddVertex(v2Vertex);
			graf.AddVertex(v3Vertex);
			graf.AddVertex(v4Vertex);
			graf.AddVertex(v5Vertex);

			var res = graf.GetAllPairsShortestsPathsJohnson();

			Assert.Equal(0, res[0, 0]);
			Assert.Equal(3, res[1, 0]);
			Assert.Equal(7, res[2, 0]);
			Assert.Equal(2, res[3, 0]);
			Assert.Equal(8, res[4, 0]);

			Assert.Equal(1, res[0, 1]);
			Assert.Equal(0, res[1, 1]);
			Assert.Equal(4, res[2, 1]);
			Assert.Equal(-1, res[3, 1]);
			Assert.Equal(5, res[4, 1]);

			Assert.Equal(-3, res[0, 2]);
			Assert.Equal(-4, res[1, 2]);
			Assert.Equal(0, res[2, 2]);
			Assert.Equal(-5, res[3, 2]);
			Assert.Equal(1, res[4, 2]);

			Assert.Equal(2, res[0, 3]);
			Assert.Equal(1, res[1, 3]);
			Assert.Equal(5, res[2, 3]);
			Assert.Equal(0, res[3, 3]);
			Assert.Equal(6, res[4, 3]);

			Assert.Equal(-4, res[0, 4]);
			Assert.Equal(-1, res[1, 4]);
			Assert.Equal(3, res[2, 4]);
			Assert.Equal(-2, res[3, 4]);
			Assert.Equal(0, res[4, 4]);
		}
	}
}
