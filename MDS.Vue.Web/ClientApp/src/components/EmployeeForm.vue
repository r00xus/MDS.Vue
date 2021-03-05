<template>
    <h4 v-if="id != 0">Изменить данные сотрудника</h4>
    <h4 v-if="id == 0">Создать данные сотрудника</h4>

    <div style="margin-top:20px;">
        <div class="form-group">
            <div class="form-group">
                <label for="name">Фамилия имя отчество</label>
                <input id="name" v-model="orgUnit.name" type="text" class="form-control">
                <div class="error">
                    {{ errors.name }}
                </div>
            </div>
            <div class="form-group">
                <label for="position">Должность</label>
                <input id="position" v-model="orgUnit.position" type="text" class="form-control">
                <div class="error">
                    {{ errors.position }}
                </div>
            </div>
            <div class="form-group">
                <label for="isHeadOfOrgUnit">
                    <input id="isHeadOfOrgUnit" v-model="orgUnit.isHeadOfOrgUnit" type="checkbox">
                    Руководитель
                </label>
            </div>
        </div>
        <p style="text-align:right;">
            <button @click="save" class="btn btn-success">Сохранить</button>
            <button @click="back" class="btn btn-secondary" style="margin-left:10px">Назад к списку</button>
        </p>
    </div>
</template>

<script>
    import router from '../router'
    import axios from 'axios'
    import { useRoute } from 'vue-router'

    export default {
        name: "EmployeeForm",
        data() {
            return {
                id: 0,
                departmentId: null,
                orgUnit: {
                    id: 0,
                    position: null,
                    isHeadOfOrgUnit: false,
                    parentId: null,
                    name: null,
                    type: 2
                },
                errors: {
                    name: null
                }
            }
        },
        methods: {
            back() {
                router.push({
                    name: 'employees',
                    params: {
                        id: this.departmentId
                    }
                });
            },
            getEmployee(id) {

                axios.get('/api/OrgUnits/' + id)
                    .then((response) => {
                        this.orgUnit = response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            save() {

                if (!this.isValid()) return;

                if (this.id == 0)
                    this.create()
                else
                    this.edit(this.id)
            },
            create() {

                this.orgUnit.parentId = this.departmentId;

                axios.post('/api/OrgUnits/', this.orgUnit)
                    .then(() => {
                        this.back();
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            edit(id) {

                axios.put('/api/OrgUnits/' + id, this.orgUnit)
                    .then(() => {
                        router.push({
                            name: 'employees',
                            params: {
                                id: this.orgUnit.parentId
                            }
                        });
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            isValid() {

                this.errors.name = null;
                this.errors.position = null;

                var result = true;

                if (this.orgUnit.name == "" || this.orgUnit.name == null) {
                    this.errors.name = "Заполните поле!";
                    result = false;
                }

                if (this.orgUnit.position == "" || this.orgUnit.position == null) {
                    this.errors.position = "Заполните поле!";
                    result = false;
                }

                return result;
            }
        },
        mounted() {

            const route = useRoute()

            this.id = route.params.id === undefined ? 0 : route.params.id;
            this.departmentId = route.params.departmentId;

            if (this.id != 0)
                this.getEmployee(this.id);

        }
    }
</script>