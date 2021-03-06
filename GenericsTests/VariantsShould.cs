﻿using Generics;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GenericsTests
{
    public class VarianceTests
    {
        Workers<Developer> _developers;

        public VarianceTests()
        {
            _developers = new Workers<Developer>();

            _developers.Add(new Developer { Name = "Bob" });
            _developers.Add(new Developer { Name = "Tom" });
            _developers.Add(new CSharpDeveloper { Name = "Amy" });
        }

        [Fact]
        public void AllowInvocationWithMoreDerivedTypeForCovariantMethod()
        {
            // illegal - Workers<T> is invariant, so we can't convert
            // Workers<Employee> e = _developers;

            // illegal - can't implicitly convert to more derived type
            //IReadOnlyEmployeeCollection<CSharpDeveloper> csDevs = _developers;

            // implicitly convert to less derived type (acceptable conversion)
            // so although the underlying collection is of type Developer,
            // we can use it as though it were type Employee
            // experiment: remove out modifier from interface declaration
            IReadOnlyEmployeeCollection<Employee> employees = _developers;

            foreach (var employee in employees.GetAll())
            {
                Console.WriteLine(employee.DoWork());
            }
        }

        [Fact]
        public void ReturnMoreDerivedTypeFromContravariant()
        {
            // illegal - Workers<T> is invariant, so we can't convert
            //Workers<CSharpDeveloper> workers = developers;
            
            // illegal - can't implicitly convert to less derived type
            // IWriteOnlyEmployeeCollection<Employee> employeesWriteCollection = _developers;

            // implicitly convert to a more derived type (acceptable conversion)
            // so although the underlying collection is of type Developer,
            // we can use it as though it were of type CSharpDeveloper
            // experiment: remove in modifier from interface declaration
            IWriteOnlyEmployeeCollection<CSharpDeveloper> csharpDevelopers = _developers;
            csharpDevelopers.Add(new CSharpDeveloper { Name = "Brandon" });
        }

    }
}
