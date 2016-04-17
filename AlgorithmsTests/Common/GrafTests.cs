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
	}
}
