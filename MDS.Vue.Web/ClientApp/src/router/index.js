import { createWebHistory, createRouter } from "vue-router";
import DirectionList from "@/components/DirectionList.vue";
import DepartmentList from "@/components/DepartmentList.vue";
import EmployeeList from "@/components/EmployeeList.vue";
import DirectionForm from "@/components/DirectionForm.vue";
import DepartmentForm from "@/components/DepartmentForm.vue";
import EmployeeForm from "@/components/EmployeeForm.vue";

const routes = [
    {
        path: "/",
        name: "directions",
        component: DirectionList,
    },
    {
        path: "/direction/create",
        name: "createDirection",
        component: DirectionForm,
    },
    {
        path: "/direction/edit/:id",
        name: "editDirection",
        component: DirectionForm,
    },
    {
        path: "/departments/:id",
        name: "departments",
        component: DepartmentList,
    },
    {
        path: "/department/create/:directionId",
        name: "createDepartment",
        component: DepartmentForm,
    },
    {
        path: "/department/edit/:id",
        name: "editDepartment",
        component: DepartmentForm,
    },
    {
        path: "/employees/:id",
        name: "employees",
        component: EmployeeList,
    },
    {
        path: "/employee/create/:departmentId",
        name: "createEmployee",
        component: EmployeeForm,
    },
    {
        path: "/employee/edit/:id",
        name: "editEmployee",
        component: EmployeeForm,
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;